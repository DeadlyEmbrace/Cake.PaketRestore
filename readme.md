## Cake.PaketRestore

[![Build status](https://ci.appveyor.com/api/projects/status/gs16wo0piv3i4qo8?svg=true)](https://ci.appveyor.com/project/DeadlyEmbrace/cake-paketrestore)
[![NuGet](https://img.shields.io/nuget/v/Cake.PaketRestore.svg)](https://www.nuget.org/packages/Cake.PaketRestore/)
[![Coverage Status](https://coveralls.io/repos/github/NinetailLabs/Cake.PaketRestore/badge.svg?branch=master)](https://coveralls.io/github/NinetailLabs/Cake.PaketRestore?branch=master)

An addin for [Cake](http://cakebuild.net/) that allows the usage of [Paket](https://fsprojects.github.io/Paket/)

This addin allows for the easy retrieval of the latest Paket bootstrapper (if required) as well as the execution of the bootstrapper.

It also allows calling restore or update on Paket

You can easily reference Cake.PaketRestore directly in your build script using Cake's Addin syntax:
```csharp
#addin nuget:?package=Cake.PaketRestore
```

### Methods
The following methods are provided:

- RetrievePaketBootloader(this ICakeContext context, DirectoryPath paketDirectory)

*Check if the Paket Bootstrapper exists and retrieve the latest version from GitHub if it doesn't*

- RetrievePaketExecutable(this ICakeContext context, DirectoryPath paketDirectory)

*Use the Paket Bootstrapper to retrieve or update the Paket executable*

- PaketRestore(this ICakeContext context, DirectoryPath paketDirectory)
- PaketRestore(this ICakeContext context, DirectoryPath paketDirectory,
            PaketRestoreSettings paketRestoreSettings)

*Execute a Paket Restore*

- PaketUpdate(this ICakeContext context, DirectoryPath paketDirectory)
- PaketUpdate(this ICakeContext context, DirectoryPath paketDirectory,
            PaketUpdateSettings paketUpdateSettings)

*Execute a Paket Update*

### Icon
[Download Document](https://thenounproject.com/search/?q=paket&i=326995) by [AlfredoCreates.com](https://thenounproject.com/AlfredoCreates/) from [The Noun Project](https://thenounproject.com/), remixed with the Cake icon.