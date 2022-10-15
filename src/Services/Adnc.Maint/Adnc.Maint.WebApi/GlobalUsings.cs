global using System;
global using System.Net;
global using System.Linq;
global using System.Reflection;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq.Expressions;
global using System.Collections.ObjectModel;

global using NLog;
global using NLog.Web;
global using AutoMapper;
global using FluentValidation;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;
global using Adnc.Infra.Core.System.Extensions.Collection;
global using MongoDB.Driver;

global using Adnc.Shared;
global using Adnc.Infra.EventBus.RabbitMq;
global using Adnc.Infra.Core.System.Extensions.String;
global using Adnc.Maint.Application.Contracts.Services;
global using Adnc.Shared.Application.Contracts.Dtos;
global using Adnc.Shared.WebApi.Authorization;
global using Adnc.Infra.Caching.Interceptor;
global using Adnc.Infra.IdGenerater.Yitter;
global using Adnc.Maint.Application.Services.Caching;
global using Adnc.Shared.Application.Services;
global using Adnc.Maint.Application.Contracts.Dtos;
global using Adnc.Shared.Application.Contracts.Attributes;
global using Adnc.Shared.Application.Contracts.Interfaces;
global using Adnc.Shared.Application.Contracts.ResultModels;
global using Adnc.Shared.Consts.Caching.Maint;
global using Adnc.Shared.Consts.Entity.Maint;
global using Adnc.Shared.Application.Contracts.Dtos.Inputs;
global using Adnc.Shared.Application.Contracts.Dtos.Outputs;
global using Adnc.Shared.Application.Contracts.Dtos.Searchs;
global using Adnc.Shared.Repository.EfEntities.Config;
global using Adnc.Infra.Repository.Entities;
global using Adnc.Shared.Repository.EfEntities;
global using Adnc.Infra.Caching.Core.Interceptor;
global using Adnc.Shared.Consts.Caching.Usr;
global using Adnc.Shared.Application.Contracts.Vos;
global using Adnc.Infra.Repository.IRepositories.Models;
global using Adnc.Shared.WebApi.Controller;
global using Adnc.Infra.Caching.Configurations;
global using Adnc.Shared.WebApi.Registrar;
global using Adnc.Infra.Caching;
global using Adnc.Shared.Application.Caching;
global using Adnc.Infra.Core.Adnc.Interfaces;
global using Adnc.Infra.Repository.IRepositories;
global using Adnc.Maint.Entities;
global using Adnc.Shared.Consts.Mq;
global using System.Text.Json;

global using Adnc.Shared.Repository.MongoEntities;
global using Adnc.Shared.Application.BloomFilter;
global using CachingConsts = Adnc.Shared.Consts.Caching.Maint.CachingConsts;



