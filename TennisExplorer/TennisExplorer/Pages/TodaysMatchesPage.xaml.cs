﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TennisExplorer.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodaysMatchesPage : ContentPage
	{
		public TodaysMatchesPage()
		{
			InitializeComponent ();
		}
	}
}