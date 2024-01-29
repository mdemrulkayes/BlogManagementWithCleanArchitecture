# Blog Management With clean architecture and domain-driven design

### <ins>[Project Overview](#)</ins>
An experimental API project that uses Domain-Driven Design (DDD) principles and Clean Architecture to manage a blog. The project uses various tools and technologies, including [**ASP.NET Core 8 Identity**](https://devblogs.microsoft.com/dotnet/improvements-auth-identity-aspnetcore-8/) to manage users and roles. I have implemented a basic version of the Identity framework without any changes. Users can create categories, tags, and blog posts using DDD principles. To improve performance, I have avoided exception throwing without unhandled exceptions and used the Result pattern to provide clearer aspects of known errors and convert those errors to the [**ProblemDetails pattern**](https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/).

<p float="left">
  <img src="https://github.com/mdemrulkayes/BlogManagementWithCleanArchitecture/assets/9307157/b31f00e3-7bfa-4559-8fe2-7063f3cd1e90" alt="clean architecture" height="400" width="400" />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://github.com/mdemrulkayes/BlogManagementWithCleanArchitecture/assets/9307157/3d44debc-54f2-489c-9974-63cf8c30cbd6" alt="solution structure" height="400" width="550" />
</p>


### <ins>[Tools & Technologies](#)</ins>
<ul>
  <li>Visual Studio 2022</li>
  <li>ASP.NET Core 8</li>
  <li>Minimal APIs</li>
  <li>ASP.NET Core Indentity</li>
  <li>Domain-Driven Design</li>
  <li>Clean Architecture</li>
  <li>Structured Logging with Serilog & Seq</li>
  <li>MediatR</li>
  <li>FluentValidation</li>
  <li>AutoMapper</li>
  <li>xUnit</li>
  <li>Docker</li>
</ul>
