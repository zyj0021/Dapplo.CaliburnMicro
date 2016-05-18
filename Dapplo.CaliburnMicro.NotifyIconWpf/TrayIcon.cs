﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
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
//  along with Dapplo.CaliburnMicro If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;

#endregion

namespace Dapplo.CaliburnMicro.NotifyIconWpf
{
	/// <summary>
	/// This is the TrayIcon, which makes the TaskbarIcon usable from Caliburn
	/// </summary>
	public class TrayIcon : TaskbarIcon, ITrayIcon
	{
		/// <summary>
		///     Show the icon
		/// </summary>
		public void Show()
		{
			Visibility = Visibility.Visible;
		}

		/// <summary>
		///     Hide the icon
		/// </summary>
		public void Hide()
		{
			Visibility = Visibility.Collapsed;
		}

		/// <summary>
		///     Show a custom balloon (ViewModel first), using the specified animation.
		///     After the timeout, the balloon is removed.
		/// </summary>
		/// <typeparam name="T">Type for the ViewModel to show</typeparam>
		/// <param name="animation">PopupAnimation</param>
		/// <param name="timeout">TimeSpan</param>
		public void ShowBalloonTip<T>(PopupAnimation animation = PopupAnimation.Slide, TimeSpan? timeout = default(TimeSpan?))
		{
			var rootModel = IoC.Get<T>();

			var view = ViewLocator.LocateForModel(rootModel, null, null);
			ViewModelBinder.Bind(rootModel, view, null);
			ShowCustomBalloon(view, animation, timeout.HasValue ? (int)timeout.Value.TotalMilliseconds : (int?)null);
		}
	}
}