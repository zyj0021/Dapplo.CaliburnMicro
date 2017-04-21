﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2017 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.CaliburnMicro
// 
//  Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Addons;
using Dapplo.Ini;

#endregion

namespace Dapplo.CaliburnMicro.Configuration
{
    /// <summary>
    ///     This registers a ServiceProviderExportProvider for providing IIniSection
    /// </summary>
    [StartupAction(StartupOrder = int.MinValue)]
    public class ConfigurationStartup : IAsyncStartupAction
    {
        [Import]
        private IApplicationBootstrapper ApplicationBootstrapper { get; set; }

        /// <summary>
        /// async Start of the IniConfig
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task</returns>
        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var iniConfig = IniConfig.Current;
            if (iniConfig == null)
            {
                iniConfig = new IniConfig(ApplicationBootstrapper.ApplicationName, ApplicationBootstrapper.ApplicationName);
                await iniConfig.LoadIfNeededAsync(cancellationToken);
            }
            ApplicationBootstrapper.Export<IServiceProvider>(iniConfig);
        }
    }
}