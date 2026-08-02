# Copilot Instructions

## Project Guidelines
- Repository targets .NET Framework 4.8 with C# 9 and prefers enabling nullable reference types in source files. Designer .Designer.cs files should use '#nullable enable' when controls are declared with nullable annotations (e.g., Button?). CI should fail only on the curated warning list: CS8602;CS8604;CS8618;CS8625;CS8629;CS8632;CS0105;CS0219;CS0168;CS1998. Continue fixing CS8669 by adding '#nullable enable' to Designer files and then address remaining CS86xx warnings via null checks or safe fallbacks.