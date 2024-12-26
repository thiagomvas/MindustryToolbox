# Mindustry Toolbox  
Welcome to the Mindustry Toolbox! This platform is designed to help Mindustry players with a collection of tools to enhance gameplay and make managing your game easier. Whether you're a seasoned player or just starting out, these tools are built to be simple and easy to use for everyone.

> [!NOTE]  
> This toolbox is fan-made and is not an official tool provided by the game developer.

The website version is available here: [Mindustry Toolbox](https://thiagomvas.github.io/MindustryToolbox/)

---

## 🛠️ Available Tools  

- **Sectors Map**:  
  Easily locate sectors on Serpulo by applying resource filters to find what you need.  

- **Resources Calculator (WIP but usable)**:  
  Calculate the resources required to produce a specific amount of an item every second.  

---

## 🧑‍🤝‍🧑 Contributing  

There are plenty of ways you can contribute to improving the Mindustry Toolbox:  

1. **Report Issues**  
   - Some sector data might be incorrect. If you find any mistakes while using the tools, please create an issue on GitHub to let me know so I can fix it.  

2. **Share Feedback**  
   - Ideas, suggestions, and overall feedback are always appreciated. If you have new tool ideas or features you'd like to see, don't hesitate to share them!  

3. **Code Contributions**  
   - If you're feeling fancy and know how to code (especially in **C#** and **Blazor**), you're welcome to contribute your own features or fixes to the project.  

---

## 💡 How to Get Started  

- **Using the Tools**: Simply visit the website and start exploring the tools.  
- **Submitting Feedback or Issues**: Head to the GitHub repository(this very page!) and create an issue. Be as detailed as possible to help with resolving it.  
- **Contributing Code**: Fork the repository, make your changes, and submit a pull request.  

---

## 📟 Using the CLI Tool  
For the people that are interested in the CLI version,  you will need to build and install the tool yourself:  
> [!IMPORTANT]
> You will need the .NET 8 SDK or newer to build the CLI

> [!WARNING]
> The tool is not completely stable or fully supported, be aware that it will be a bit behind on the website version, with features that might not even be present at all

1. **Clone the Repository**  
   - Use the following command to clone the GitHub repository:  
     ```bash
     git clone https://github.com/thiagomvas/MindustryToolbox.git
     ```

2. **Navigate to the CLI Directory**  
   - Inside the cloned repository, navigate to the directory for the CLI tool:  
     ```bash
     cd MindustryToolbox/MindustryToolbox.CLI
     ```

3. **Build the Project**  
   - Ensure you have the required tools installed (e.g., .NET SDK). Then, build the project using:  
     ```bash
     dotnet build -c Release
     ```

4. **Installing the tool**  
   - After building, you can install the tool using:  
     ```bash
     dotnet tool install --global --add-source ./bin/Release MindustryToolbox.CLI
     ```
5. **Using the tool**
   - After installing and if no errors were displayed, you should be able to run the program by simply using ``mindustry`` (use ``mindustry -h`` for help)

> [!NOTE]
> Additional instructions and usage details will be provided as the CLI tool becomes more stable. For now, experiment with its features and share your feedback!

