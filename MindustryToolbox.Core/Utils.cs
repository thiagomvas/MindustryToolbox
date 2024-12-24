﻿using MindustryToolbox.Core.Entities;
using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MindustryToolbox.Core
{
    internal static class Utils
    {
        public static List<Sector> ParseSectors(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var sectors = new List<Sector>();

            foreach(var line in lines)
            {
                var parts = line.Split('\t');
                string name = parts[0];
                SectorType type = parts[1].ToUpper() switch
                {
                    "ATTACK" => SectorType.Attack,
                    "SURVIVAL" => SectorType.Survival,
                    _ => SectorType.Survival
                };
                Threat threat = ParseThreat(parts[2]);
                // Get resources related parts (index 3-12)
                Resource resources = ParseResources(parts);
                int numOfWaves = int.TryParse(parts[13], out int waveNum) ? waveNum : -1;

                var result = new Sector
                {
                    Name = name,
                    Type = type,
                    Threat = threat,
                    Resources = resources,
                    NumOfWaves = numOfWaves,
                    VulnerableTo = ParseVulnerableTo(parts[14]),
                    Planet = Planet.Serpulo
                };

                sectors.Add(result);
            }

            return sectors;
        }

        public static Threat ParseThreat(string threatLevel)
        {
            return threatLevel.ToUpper() switch
            {
                "LOW" => Threat.Low,
                "MEDIUM" => Threat.Medium,
                "HIGH" => Threat.High,
                "EXTREME" => Threat.Extreme,
                "ERADICATION" => Threat.Eradication,
                _ => Threat.Low
            };
        }

        public static Resource ParseResources(string[] parts)
        {
            Resource resources = Resource.None;

            // Map columns (3-11) to Resource flags (ensure parts are within range)
            if (parts.Length >= 12)
            {
                if (!string.IsNullOrWhiteSpace(parts[3] ))  resources |= Resource.Copper;
                if (!string.IsNullOrWhiteSpace(parts[4] ))  resources |= Resource.Lead;
                if (!string.IsNullOrWhiteSpace(parts[5] ))  resources |= Resource.Sand;
                if (!string.IsNullOrWhiteSpace(parts[6] ))  resources |= Resource.Coal;
                if (!string.IsNullOrWhiteSpace(parts[7] ))  resources |= Resource.Titanium;
                if (!string.IsNullOrWhiteSpace(parts[8] ))  resources |= Resource.Thorium;
                if (!string.IsNullOrWhiteSpace(parts[9] ))  resources |= Resource.Scrap;
                if (!string.IsNullOrWhiteSpace(parts[10])) resources |= Resource.Water;
                if (!string.IsNullOrWhiteSpace(parts[11])) resources |= Resource.Oil;
                if (!string.IsNullOrWhiteSpace(parts[12])) resources |= Resource.Magma;
            }

            return resources;
        }

        public static string[] ParseVulnerableTo(string vulnerableTo)
        {
            if (string.IsNullOrWhiteSpace(vulnerableTo) || vulnerableTo == "None") return Array.Empty<string>();

            // Split by comma and ampersand, then trim each element
            return vulnerableTo.Split(new[] { ',', '&' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(v => v.Trim())
                               .ToArray();
        }
    }
}
