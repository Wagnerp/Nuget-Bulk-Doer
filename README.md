# NuGet Bulk Doer

## Description

Want to unlist some or all of your project versions in one easy interactive tool? Hope to keep your packages secure and clean by unlisting everything you don't want visitors to see? Don't want to spend hours individually unlisting your versions by hand?

`nugetbulkdoer` is the solution! It is a global .NET CLI tool that provides an easy interface to unlist some or all versions of your project.

## Installation

`nugetbulkdoer` is available as a .NET Core Global Tool:

```bash
dotnet tool install --global NugetBulkDoer --version 2.2.0
```
The latest version can also be downloaded directly from NuGet.org at:
https://www.nuget.org/packages/NugetBulkDoer/

## Prerequisites

`nugetbulkdoer` can be used on any machine with the .NET CLI installed.

## How It Works

`nugetbulkdoer` allows you to Unlist some, all, or only the preview versions by automating the .NET CLI call depending on your inputs. 

## Usage

```bash
nugetbulkdoer
```

You will then be prompted for your API key and the package ID. 

```bash
Provide the API key associated with this package. 
Make sure you have created the key with unlisting privileges.
>
Provide the ID of the package you wish to unlist from. 
>
```

Finally, you will be asked:

```bash
Which versions would you like to unlist? (Options: some/all/previews)
>
```

### Some

`some` allows you to search your versions for a particular character or substring. This allows you to Unlist a particular range of versions.

#### Example 1: Unlist all 1.* versions

```bash
Which versions would you like to unlist? (Options: some/all/previews)
>some
Please enter the character or substring to search for.
>1.
1.0.0
1.0.1
Please confirm to unlist the above versions (y/n)
>y
```
`nugetbulkdoer` will then unlist all versions that match the pattern '1.*'.

### All

##### Example 2: Unlist all versions.

```bash
Which versions would you like to unlist? (Options: some/all/previews) 
>all
1.0.0
1.0.1
2.0.0
Please confirm to unlist the above versions (y/n)
>y
Version 1.0.0 has been unlisted.
Output from other process
Microsoft Windows [Version 10.0.19041.508]
Output from other process
(c) 2020 Microsoft Corporation. All rights reserved.
Output from other process
```

### All

##### Example 2: Unlist all preview versions.

`nugetbulkdoer` will find all preview versions, as denoted by the reserved character `-`.

```bash
Which versions would you like to unlist? (Options: some/all/previews) 
>previews
1.0.0-preview
1.0.1-preview
2.0.0-preview
Please confirm to unlist the above versions (y/n)
>y
Version 1.0.0-preview has been unlisted.
Output from other process
Microsoft Windows [Version 10.0.19041.508]
Output from other process
(c) 2020 Microsoft Corporation. All rights reserved.
Output from other process
```
