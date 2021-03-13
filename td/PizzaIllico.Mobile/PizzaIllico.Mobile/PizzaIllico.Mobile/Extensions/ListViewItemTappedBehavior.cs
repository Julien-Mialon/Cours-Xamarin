using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Extensions
{
	public class ListViewItemTappedBehavior : EventBehavior<ListView, ListViewItemTappedBehavior>
	{
		protected override void SubscribeEvent(ListView bindable)
		{
			bindable.ItemTapped += OnItemTapped;
		}
		
		protected override void UnsubscribeEvent(ListView bindable)
		{
			bindable.ItemTapped -= OnItemTapped;
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			TriggerCommand(e.Item);
		}
	}
}
