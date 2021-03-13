using System;
using System.Windows.Input;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Extensions
{
    public abstract class EventBehavior<TView, TBehavior> : BehaviorBase<TView> where TView : BindableObject
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(TBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(TBehavior), null);

        public static ICommand GetCommand(BindableObject target) => (ICommand) target.GetValue(CommandProperty);
        public static void SetCommand(BindableObject target, ICommand value) => target.SetValue(CommandProperty, value);

        public static object GetCommandParameter(BindableObject target) => target.GetValue(CommandParameterProperty);
        public static void SetCommandParameter(BindableObject target, object value) => target.SetValue(CommandParameterProperty, value);
        
        protected override void OnAttachedTo(TView bindable)
        {
            base.OnAttachedTo(bindable);
            SubscribeEvent(bindable);
        }

        protected override void OnDetachingFrom(TView bindable)
        {
            UnsubscribeEvent(bindable);
            base.OnDetachingFrom(bindable);
        }

        protected virtual void TriggerCommand()
        {
            ICommand command = GetCommand(this);
            command?.Execute(GetCommandParameter(this));
        }
        
        protected virtual void TriggerCommand(object parameter)
        {
            ICommand command = GetCommand(this);
            command?.Execute(parameter);
        }
        
        protected abstract void SubscribeEvent(TView bindable);
        protected abstract void UnsubscribeEvent(TView bindable);
    }
}