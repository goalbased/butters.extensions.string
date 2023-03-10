# Butters.Extensions.String - a string extension for .NET

<!--Try to remove appveyor [![Build status](https://ci.appveyor.com/api/projects/status/8xim58xuo7s59e61?svg=true)](https://ci.appveyor.com/project/goalbased/butters-extensions-string) -->

[![Build](https://github.com/goalbased/butters.extensions.string/actions/workflows/main.yml/badge.svg)](https://github.com/goalbased/butters.extensions.string/actions/workflows/main.yml)
[![codecov](https://codecov.io/github/goalbased/butters.extensions.string/branch/main/graph/badge.svg?token=J0EZ8CIGGQ)](https://codecov.io/github/goalbased/butters.extensions.string)

## Release Notes

Located at [goalbased.github.io/butters.extensions.string](https://goalbased.github.io/butters.extensions.string/)

## Packages

| Package                                                                                | NuGet Stable                                                                                                                                            | NuGet Pre-release                                                                                                                                      | Downloads                                                                                                                                                |
| -------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [butters.extensions.string](https://www.nuget.org/packages/Butters.Extensions.String/) | [![butters.extensions.string](https://img.shields.io/nuget/v/Butters.Extensions.String.svg)](https://www.nuget.org/packages/Butters.Extensions.String/) | [![butters.extensions.string](https://img.shields.io/nuget/vpre/Butters.Extensions.String)](https://www.nuget.org/packages/Butters.Extensions.String/) | [![butters.extensions.string](https://img.shields.io/nuget/dt/Butters.Extensions.String.svg)](https://www.nuget.org/packages/Butters.Extensions.String/) |

## Features

Butters.Extensions.String is a [NuGet library](https://www.nuget.org/packages/Butters.Extensions.String) that you can add in to your project that will extend your `string` class which provide faster and many useful extensions.

## Usage

```csharp
using Butters.Extensions.String;

var expected = "name_of_property";
var str = "nameOfProperty"

Assert.Equal(expected, str.ToSnakeCase());
```

## String Extension functions

| Name              | Description                                                                                                                 |
| ----------------- | --------------------------------------------------------------------------------------------------------------------------- |
| ToByte            | Convert string to byte array                                                                                                |
| ToBoolean         | Converts string to boolean which is case insensitive <br/>true: "true", "t", "yes", "y" <br/>false: "false", "f", "no", "n" |
| ToShort           | Converts string to 16-bit signed integer                                                                                    |
| ToInt16           | This is as same as ToShort()                                                                                                |
| <code>ToInt<code> |                                                                                                                             |
| `ToInt32`         |                                                                                                                             |

## Performance

A key feature of Butters.Extensions.String is performance.
