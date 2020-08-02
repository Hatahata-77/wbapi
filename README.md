# ASP.Net Core でスキャフォールディングを使用して、データベースから WebAPIを構築するまで

C#（ASP.Net Core） で dotnet-cli を使用して WebAPIを構築してみます。

## プロジェクトを作成する

プロジェクトを新規に作成します。
以下のコマンドでプロジェクトの種類を検索してみます。

```PowerShell
dotnet new --list
Templates                                         Short Name               Language          Tags
----------------------------------------------------------------------------------------------------------------------------------
Console Application                               console                  [C#], F#, VB      Common/Console
Class library                                     classlib                 [C#], F#, VB      Common/Library
WPF Application                                   wpf                      [C#]              Common/WPF
WPF Class library                                 wpflib                   [C#]              Common/WPF
WPF Custom Control Library                        wpfcustomcontrollib      [C#]              Common/WPF
WPF User Control Library                          wpfusercontrollib        [C#]              Common/WPF
Windows Forms (WinForms) Application              winforms                 [C#]              Common/WinForms
Windows Forms (WinForms) Class library            winformslib              [C#]              Common/WinForms
Worker Service                                    worker                   [C#]              Common/Worker/Web
Unit Test Project                                 mstest                   [C#], F#, VB      Test/MSTest
NUnit 3 Test Project                              nunit                    [C#], F#, VB      Test/NUnit
NUnit 3 Test Item                                 nunit-test               [C#], F#, VB      Test/NUnit
xUnit Test Project                                xunit                    [C#], F#, VB      Test/xUnit
Razor Component                                   razorcomponent           [C#]              Web/ASP.NET
Razor Page                                        page                     [C#]              Web/ASP.NET
MVC ViewImports                                   viewimports              [C#]              Web/ASP.NET
MVC ViewStart                                     viewstart                [C#]              Web/ASP.NET
Blazor Server App                                 blazorserver             [C#]              Web/Blazor
Blazor WebAssembly App                            blazorwasm               [C#]              Web/Blazor/WebAssembly
ASP.NET Core Empty                                web                      [C#], F#          Web/Empty
ASP.NET Core Web App (Model-View-Controller)      mvc                      [C#], F#          Web/MVC
ASP.NET Core Web App                              webapp                   [C#]              Web/MVC/Razor Pages
ASP.NET Core with Angular                         angular                  [C#]              Web/MVC/SPA
ASP.NET Core with React.js                        react                    [C#]              Web/MVC/SPA
ASP.NET Core with React.js and Redux              reactredux               [C#]              Web/MVC/SPA
Razor Class Library                               razorclasslib            [C#]              Web/Razor/Library/Razor Class Library
ASP.NET Core Web API                              webapi                   [C#], F#          Web/WebAPI
ASP.NET Core gRPC Service                         grpc                     [C#]              Web/gRPC
dotnet gitignore file                             gitignore                                  Config
global.json file                                  globaljson                                 Config
NuGet Config                                      nugetconfig                                Config
Dotnet local tool manifest file                   tool-manifest                              Config
Web Config                                        webconfig                                  Config
Solution File                                     sln                                        Solution
Protocol Buffer File                              proto                                      Web/gRPC
```

今回作成したい WebAPIのプロジェクトは　WebAPIということが分かりましたので、以下のコマンドでプロジェクトを作成します。

```PowerShell
dotnet new webapi --name wbapi
```

## 必要なツールをインストールする

### 1.Entity Framework Core ツールのインストール

以下のコマンドでインストーるします。
グローバルにインストールします。

```powershell
dotnet tool install --global dotnet-ef
```

既にインストールしている場合は以下のコマンドで最新にします。

```powershell
dotnet tool update --global dotnet-ef
```

```powershell
dotnet ef
```

で

```powershell

                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 3.1.6

Usage: dotnet ef [options] [command]

Options:
  --version        Show version information
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  database    Commands to manage the database.
  dbcontext   Commands to manage DbContext types.
  migrations  Commands to manage migrations.

Use "dotnet ef [command] --help" for more information about a command.
```

という表示がされれば成功です。

### 2.aspnet-codegenerator のインストール

以下のコマンドでインストールします。

```powershell
dotnet tool install --global dotnet-aspnet-codegenerator
```

既にインストールしている場合は以下のコマンドで最新にします。

```powershell
dotnet tool update --global dotnet-aspnet-codegenerator
```

## nuget パッケージのインストール

以下のコマンドでそれぞれインストールします。

```powershell
dotnet add package Microsoft.EntityframeworkCore.Design
dotnet add package Microsoft.EntityframeworkCore.Sqlite
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

- Microsoft.EntityframeworkCore.Design  
この後出てくる dotnet ef コマンドでデータベースからモデルを作成する際に使用します。

- Microsoft.EntityframeworkCore.Sqlite  
今回のプログラムはデータベースにSQLiteを使用します。  
他のDBMSを使用する場合はDBMSに合ったパッケージに変更するだけです。

- Microsoft.VisualStudio.Web.CodeGeneration.Design  
この後出てくる dotnet aspnet-codegenerator コマンドでモデルからコントローラーを作成する際に使用します。

- dotnet add package Microsoft.EntityFrameworkCore.SqlServer  
正直なぜ必要なのか分かりませんが、無いと dotnet aspnet-codegenerator コマンドでエラーになったため、インストールしました。

## データベースからモデルの作成

以下のコマンドを実行します。

```powershell
dotnet ef dbcontext scaffold "Data Source=wb.sqlite" Microsoft.EntityframeworkCore.Sqlite -o Models
```

-o オプションは出力ディレクトリを指定します。

## モデルからコントローラーの作成

以下のコマンドを実行します。

```powershell
dotnet aspnet-codegenerator controller -name wbController -async -api -m Wb -dc wbContext -outDir Controllers
```

-name コントローラーの名前  
-async async
-api api
-m Wb モデルクラス
-dc DBコンテキスト
-outDir 出力ディレクトリ

## Startup に DBコンテキストを追加する処理を記述

Startup.cs の ConfigureServices メソッドに以下を追記します。

```C#:startup.cs
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Models.wbContext>(); //この行
        }
```
## launchSettings.jsonの編集

launchSettings.json の　launchUrl　を今回作成した COntrollerにルーティングされるよう修正します。  
具体的には launchUrl の部分です。

```JSON:Properties/launchSettings.json
    "sample2": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "api/wb",
      "applicationUrl": "https://localhost:9999;http://localhost:9998",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
```

以上で終了です。  
実行するとブラウザで　WebAPI より取得した json が表示されると思います。

## 最後に

今回使用したコマンとをまとめておきます。

```powershell:apendix
dotnet tool update --global dotnet-ef
dotnet add package Microsoft.EntityframeworkCore.Design
dotnet add package Microsoft.EntityframeworkCore.Sqlite
dotnet ef dbcontext scaffold "Data Source=wb.sqlite" Microsoft.EntityframeworkCore.Sqlite -o Models
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet aspnet-codegenerator controller -name wbController -async -api -m Wb -dc wbContext -outDir Controllers
```
