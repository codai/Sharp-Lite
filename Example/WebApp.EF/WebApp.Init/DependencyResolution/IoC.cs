// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

using WebApp.EntityFrameworkProvider;
using SharpLite.Domain.DataInterfaces;
using SharpLite.EntityFrameworkProvider;
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using StructureMap;

namespace WebApp.Init.DependencyResolution 
{
    public static class IoC 
    {
        public static IContainer Initialize() 
        {
            var webAppDbContext = new SharpLite.EntityFrameworkProvider.DbContext(new WebApp.EntityFrameworkProvider.WebAppDbContext());
            ObjectFactory.Initialize(x =>
                        {
                            x.For<SharpLite.EntityFrameworkProvider.DbContext>()
                                .HttpContextScoped()
                                .Use(() => webAppDbContext); // For SharpLiteEntityFramework
                            x.For<IEntityDuplicateChecker>().Use<EntityDuplicateChecker>();
                            x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                            x.For(typeof(IRepositoryWithTypedId<,>)).Use(typeof(RepositoryWithTypedId<,>));
                        });
            return ObjectFactory.Container;
        }
    }
}