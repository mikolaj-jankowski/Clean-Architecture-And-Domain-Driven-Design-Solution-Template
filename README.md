# Clean Architecture and Domain Driven Design Template 



## Table of contents

[1. Instalation](#1-Installation) 

&nbsp;&nbsp; [1.1 Solution Template Installation](#1.1-Solution-Template-Installation) 

&nbsp;&nbsp; [1.2 Database](#1.2-Database) 

[2. Introduction](#2-Introduction)

&nbsp;&nbsp; [2.1 Motivation](#2.1-Motivation) 

[3. Domain](#3-Domain)

&nbsp;&nbsp; [3.1 Introduction](#3.1-Introduction) 

[4. Architecture](#4-Architecture)

[5. Observability](#5-Observability) 


## 1. Installation
I do recommend using CLI instead Visual Studnio Wizard when creating your application based on this template because Vistual Studio doesn't keep folders structure correctly.

### 1.1 Solution Template Installation
Install using CLI.
```
dotnet new install Clean.Architecture.And.DDD.Template::1.0.0
```
Once installed type
```
dotnet new ca-and-ddd -o "MyDreamProject"
```

### 1.2 Database

The template uses MSSQL as a database provider. Migrations will be applied automatically during project startup, so you don't have to do anything.

## 2. Introduction

### 2.1 Motivation

I couldn't find any repository that met the following criteria:

    1. Implemented Mediator pattern using MassTransit library instead of MediatR with added message interception support (similar to IPipelineBehaviour in MediatR).
    2. Fully implemented system observability using Open Telemetry.
    3. Project created according to Clean Architecture and DDD with a domain other than e-commerce (e.g., order aggregate, etc.).

Even when encountering projects that fulfilled one of these points, they often conflicted with others or omitted them entirely. Therefore, I decided to create a project that meets the above criteria. I am sharing it in case someone else is looking for something similar.

## 3. Domain

### 3.1 Introduction
Domain of e-commarce was chose intentionaly because it is well known and easy to understand. 
That template consists of just two aggregates **Customer** and **Order**. I decided to keep it small and simple but fully functional.

## 4. Architecture

## 5. Observability
Observability is one of the most important aspects that was emphasized during the creation of this project. As a result, all elements of the system that provide telemetry data have been configured to trace the lifecycle of an HTTP request from start to finish. Telemetric data is received by the Aspire Dashboard, which is responsible for their visualization.
Here are some examples of requestes

[image](1)
[image](2)
[image](3)

### 5.1 Open Telemtry


## :hammer: Build with
* [.NET Core 8](https://github.com/dotnet/core)
* [ASP.NET Core 8](https://github.com/dotnet/aspnetcore)
* [MassTransit](https://github.com/MassTransit)
