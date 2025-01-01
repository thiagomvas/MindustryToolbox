using MindustryToolbox.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryToolbox.Core.DTOs;
internal class NaturalResources
{
    public bool HasCopper { get; set; } = false;
    public bool HasLead { get; set; } = false;
    public bool HasSand { get; set; } = false;
    public bool HasCoal { get; set; } = false;
    public bool HasTitanium { get; set; } = false;
    public bool HasThorium { get; set; } = false;
    public bool HasScrap { get; set; } = false;
    public bool HasWater { get; set; } = false;
    public bool HasOil { get; set; } = false;
    public bool HasMagma { get; set; } = false;

    public Resource ToResourceFlags()
    {
        Resource resources = Resource.None;

        if (HasCopper) resources |= Resource.Copper;
        if (HasLead) resources |= Resource.Lead;
        if (HasSand) resources |= Resource.Sand;
        if (HasCoal) resources |= Resource.Coal;
        if (HasTitanium) resources |= Resource.Titanium;
        if (HasThorium) resources |= Resource.Thorium;
        if (HasScrap) resources |= Resource.Scrap;
        if (HasWater) resources |= Resource.Water;
        if (HasOil) resources |= Resource.Oil;
        if (HasMagma) resources |= Resource.Magma;

        return resources;
    }
}
