# Clean Architecture and Domain Driven Design Template 

This repository is a template for an application created using **Clean Architecture**, utilizing **building blocks from Domain-Driven Design**. Read and write operations have been separated according to the **CQRS** pattern. 
System observability is ensured by the implementation of OpenTelemetry and the Aspire Dashboard.
Additionally, you'll find implementations of design patterns such as mediator, factory, strategy, and several others.

## Table of contents

* [1. Instalation](#1-Installation) 
    * [1.1 Solution Template Installation](#11-Solution-Template-Installation) 
    * [1.2 Database](#12-Database)
    * [1.3 Docker](#13-Docker)
* [2. Introduction](#2-Introduction)
    * [2.1 Motivation](#21-Motivation) 
* [3. Domain](#3-Domain)
    * [3.1 Introduction](#31-Introduction)
    * [3.2 Aggregates](#32-Aggregates)
    * [3.3 Domain Events](#33-Domain-Events) 
* [4. Architecture](#4-Architecture)
    * [4.1 Clean Architecure](#41-Clean-Architecure)
        * [4.1.1 Presentation Layer](#411-Presentation-Layer)
        * [4.1.2 Infrastructure Layer](#412-Infrastructure-Layer)
        * [4.1.3 Application Layer](#413-Application-Layer)
        * [4.1.4 Domain Layer](#414-Domain-Layer)
    * [4.2 Eventual consistency](#42-Eventual-consistency)
        * [4.2.1 Domain Events](#421-Domain-Events)
        * [4.2.2 Integration Events](#422-Integration-Events)
    * [4.3 Command Query Responsibility Segregation (CQRS)](#43-Command-Query-Responsibility-Segregation-(CQRS))
    * [4.4 Cross Cutting Concerns](#44-Cross-Cutting-Concerns)
* [5. Observability](#5-Observability)
* [6. Design patterns implemented in this project](6-Design-patterns-implemented-in-this-project)
    * [6.1 Mediator](61-Mediator)
    * [6.2 Factory method](62-Factory-Method)
    * [6.3 Strategy](63-Strategy)
* [7. Tests](7-Tests)



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

### 1.3 Docker

As a result of running command from step 1.1 all files and folders will be created. Among them you will find docker-compose.yaml.
Simply run the command 'docker-compose up' to create required containers.
docker-compose.yaml provides instances of: MSSQL, Redis, RabbitMQ, and Aspire Dashboard. 

## 2. Introduction

### 2.1 Motivation

I couldn't find any repository that met the following criteria:

1. Implemented Mediator pattern **using MassTransit library instead of MediatR** with added message interception support (similar to IPipelineBehaviour in MediatR.
2. Implemented system observability using Open Telemetry.
3. Implemented Domain Events as part of Eventual Consistency, so Domain Events are not published in the same transaction as saving/updating Aggregate.

Even when encountering projects that fulfilled one of these points, they often conflicted with others or omitted them entirely. Therefore, I decided to create a project that meets the above criteria. I am sharing it in case someone else is looking for something similar.

## 3. Domain

### 3.1 Introduction
The e-commerce domain was deliberately chosen because it is widely known and understood. This template consists of two aggregates: Customer and Order. I decided to extend the domain just enough to utilize all the building blocks, but nothing more. The goal of this repository is to create a template that provides an example implementation.

### 3.2 Aggregates
### 3.3 Domain Events

## 4. Architecture

### 4.1 Clean Architecure
#### 4.1.1 Presentation Layer
#### 4.1.2 Infrastructure Layer
#### 4.1.3 Application Layer
#### 4.1.4 Domain Layer

### 4.2 Eventual consistency
### 4.2.1 Domain Events
### 4.2.2 Integration Events

### 4.3 Command Query Responsibility Segregation (CQRS)

### 4.4 Cross Cutting Concerns

## 5. Observability
Observability is one of the most important aspects that was emphasized during the creation of this project. As a result, all elements of the system that provide telemetry data have been configured to trace the lifecycle of an HTTP request from start to finish. Telemetric data is received by the Aspire Dashboard, which is responsible for their visualization.

Aspire Dashboard is avaiable at: http://localhost:18888 once docker-compose has been launched.

Here is an example of creating a customer, which results in adding a new record in the Aspire Dashboard.

![](docs/Images/PostCustomer.gif)

### 5.1 Open Telemtry

## 6 Design patterns implemented in this project
### 6.1 Mediator
### 6.2 Factory method
### 6.3 Strategy
## :hammer: Build with
* [.NET Core 8](https://github.com/dotnet/core)
* [ASP.NET Core 8](https://github.com/dotnet/aspnetcore)
* [MassTransit](https://github.com/MassTransit)

## 7. Tests
