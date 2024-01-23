# Cake.grate

[![Build][githubimage]][githubbuild]
[![NuGet package][nugetimage]][nuget]

Makes [grate](https://erikbra.github.io/grate/) available as a tool in [Cake](https://cakebuild.net/). grate is a re-write of [RoundhousE](https://github.com/chucknorris/roundhouse) for reasons detailed a [RoundhousE issue](https://github.com/chucknorris/roundhouse/issues/438).

From the grate documentation:

>grate is a SQL scripts migration runner, using plain, old SQL for migrations. No meta-language, no code, no config, no EF migrations. It gives you full flexibility, and full control of your migrations, and lets you use all the fancy features of you particular database system. You are not constrained to any lowest common feature set of all supported databases. 

## Table of Contents

- [Install](#install)
- [Usage](#usage)


## Install

```cs
#addin nuget:?package=Cake.grate
```

## Usage

```cs
#tool "dotnet:?package=grate&version=1.5.4"
#addin nuget:?package=Cake.grate

Task("MyTask").Does(() => {
  Grate(new GrateSettings()
    {
        ConnectionString = "Server=(local);Database=mydatabase;Trusted_Connection=True;TrustServerCertificate=true;"
    });
});
```
[githubbuild]: https://github.com/cake-contrib/Cake.grate/actions/workflows/build.yml?query=branch%3Amain
[githubimage]: https://github.com/cake-contrib/Cake.grate/actions/workflows/build.yml/badge.svg?branch=main
[nuget]: https://nuget.org/packages/Cake.grate
[nugetimage]: https://img.shields.io/nuget/v/Cake.grate.svg?logo=nuget&style=flat-square