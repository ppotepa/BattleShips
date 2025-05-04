# BattleShips

> **❗️ THIS PROJECT IS A RECRUITMENT TASK **

A simple C# implementation of the classic Battleship game, organized as a .NET solution with a game engine library, a console demo application, and unit tests—all targeting .NET 6.0.

## Table of Contents

- [About](#about)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Building and Running](#building-and-running)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## About

BattleShips was developed to demonstrate a clean, object-oriented design for the Battleship game, featuring:

- A reusable game-logic library (`BattleShips.Game`)
- A minimal console application (`BattleShips.Demo`)
- NUnit-based unit tests covering board setup and validation (`BattleShips.Tests`)

This project was **solely done for the purpose of recruitment**.

## Project Structure

```
/src
├── BattleShips.Game         # Core game-engine library
│   └── BattleShipsGame.cs
├── BattleShips.Demo         # Console demo application
│   └── Program.cs
└── BattleShips.Tests        # Unit tests (NUnit)
    └── GridTests.cs
BattleShips.sln              # Solution file
```

All projects target .NET 6.0.

## Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- (Optional) Visual Studio 2022 or later

## Building and Running

1. **Restore and build**  
   ```bash
   cd src
   dotnet build
   ```
2. **Run the console demo**  
   ```bash
   dotnet run --project BattleShips.Demo/BattleShips.Demo.csproj
   ```

## Testing

Execute the unit tests with:

```bash
dotnet test src/BattleShips.Tests/BattleShips.Tests.csproj
```

Tests are written using NUnit and cover grid validation and basic game-board logic.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to enhance the game logic, add AI or GUI support, or improve test coverage.

## License

This repository does not include an explicit license. If you wish to reuse any part of this code, please contact the author for permission.
