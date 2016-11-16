﻿#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.CaliburnMicro
// 
// Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System.Windows;
using System.Windows.Controls;

#endregion

namespace Dapplo.CaliburnMicro.Behaviors
{
	/// <summary>
	/// This code comes from <a href="http://www.executableintent.com/attached-behaviors-part-2-framework/">here</a>
	/// </summary>
	public static class EnumGroup
	{
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(object),
			typeof(EnumGroup),
			new PropertyMetadata(OnArgumentsChanged));

		public static readonly DependencyProperty TargetValueProperty = DependencyProperty.RegisterAttached(
			"TargetValue",
			typeof(string),
			typeof(EnumGroup),
			new PropertyMetadata(OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new EnumGroupBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		public static object GetValue(RadioButton radioButton)
		{
			return radioButton.GetValue(ValueProperty);
		}

		public static void SetValue(RadioButton radioButton, object value)
		{
			radioButton.SetValue(ValueProperty, value);
		}

		public static string GetTargetValue(RadioButton radioButton)
		{
			return (string) radioButton.GetValue(TargetValueProperty);
		}

		public static void SetTargetValue(RadioButton radioButton, string value)
		{
			radioButton.SetValue(TargetValueProperty, value);
		}

		private sealed class EnumGroupBehavior : Behavior<RadioButton>
		{
			private readonly EnumCheck _enumCheck = new EnumCheck();

			internal EnumGroupBehavior(DependencyObject host) : base(host)
			{
			}

			protected override void Attach(RadioButton host)
			{
				host.Checked += OnChecked;
			}

			protected override void Detach(RadioButton host)
			{
				host.Checked -= OnChecked;
			}

			protected override void Update(RadioButton host)
			{
				_enumCheck.Update(GetValue(host), GetTargetValue(host));

				host.IsChecked = _enumCheck.IsMatch;
			}

			private void OnChecked(object sender, RoutedEventArgs e)
			{
				TryUpdate(host => SetValue(host, _enumCheck.ParsedTargetValue));
			}
		}
	}
}