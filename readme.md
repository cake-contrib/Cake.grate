This is work in progress

This project will enable the database migration tool [grate](https://erikbra.github.io/grate/) to be used as an addin in [Cake](https://cakebuild.net). 

This addin will be backwards compatible with the existing [RoundhousE](https://cakebuild.net/dsl/roundhouse/) aliases where the tool grate is backwards compatible with RoundhousE. For details see the grate guide to [Migrating from RoundHouse](https://erikbra.github.io/grate/migrating-from-roundhouse/).

Current version of grate supported is 1.5.3 - once complete I suggest this repo follows the grate version numbers for clarity



# Cake.grate

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
