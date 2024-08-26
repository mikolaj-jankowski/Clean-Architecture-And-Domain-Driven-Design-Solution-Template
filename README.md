# Clean Architecture and Domain Driven Design Template 



## Table of contents

* [1. Instalation](#1-Installation) 

    * [1.1 Solution Template Installation](#1.1-Solution-Template-Installation) 



## 1. Installation
I do recommend using CLI instead Visual Studnio Wizard when creating your application based on this template because Vistual Studio doesn't keep folders structure correctly.

### 1 Solution Template Installation
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
The e-commerce domain was deliberately chosen because it is widely known and understood. This template consists of two aggregates: Customer and Order. I decided to extend the domain just enough to utilize all the building blocks, but nothing more. The goal of this repository is to create a template that provides an example implementation.

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
