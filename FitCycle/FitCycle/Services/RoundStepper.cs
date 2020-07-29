using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FitCycle.Services
{
    public class RoundStepper : StackLayout
    {
        ImageButton PlusBtn;
        Frame FramePlusBtn;
        Frame FrameMinusBtn;
        ImageButton MinusBtn;
        Entry TextEntry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(string),
              declaringType: typeof(RoundStepper),
              defaultValue: "0",
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderProperty =
          BindableProperty.Create(
             propertyName: "Placeholder",
              returnType: typeof(string),
              declaringType: typeof(RoundStepper),
              defaultValue: "",
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderColorProperty =
          BindableProperty.Create(
             propertyName: "PlaceholderColor",
              returnType: typeof(Color),
              declaringType: typeof(RoundStepper),
              defaultValue: Color.Gray,
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty FontSizeProperty =
          BindableProperty.Create(
             propertyName: "FontSize",
              returnType: typeof(int),
              declaringType: typeof(RoundStepper),
              defaultValue: 20,
              defaultBindingMode: BindingMode.OneWay);
        public static readonly BindableProperty StepperSizeProperty =
          BindableProperty.Create(
             propertyName: "StepperSize",
              returnType: typeof(int),
              declaringType: typeof(RoundStepper),
              defaultValue: 20,
              defaultBindingMode: BindingMode.OneWay);
        public static readonly BindableProperty StepperColorProperty =
          BindableProperty.Create(
             propertyName: "StepperColor",
              returnType: typeof(Color),
              declaringType: typeof(RoundStepper),
              defaultValue: Color.Gray,
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty IsEntryEnabledProperty =
          BindableProperty.Create(
             propertyName: "IsEntryEnabled",
              returnType: typeof(bool),
              declaringType: typeof(RoundStepper),
              defaultValue: true,
              defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty IncrementProperty =
          BindableProperty.Create(
             propertyName: "Increment",
              returnType: typeof(double),
              declaringType: typeof(RoundStepper),
              defaultValue: 1.0,
              defaultBindingMode: BindingMode.OneWay);
        public static readonly BindableProperty MaxiumumProperty =
          BindableProperty.Create(
             propertyName: "Maximum",
              returnType: typeof(double),
              declaringType: typeof(RoundStepper),
              defaultValue: 1000.0,
              defaultBindingMode: BindingMode.OneWay);
        public static readonly BindableProperty MinimumProperty =
          BindableProperty.Create(
             propertyName: "Minimum",
              returnType: typeof(double),
              declaringType: typeof(RoundStepper),
              defaultValue: 0.0,
              defaultBindingMode: BindingMode.OneWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
        public Color StepperColor
        {
            get { return (Color)GetValue(StepperColorProperty); }
            set { SetValue(StepperColorProperty, value); }
        }
        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public int StepperSize
        {
            get { return (int)GetValue(StepperSizeProperty); }
            set { SetValue(StepperSizeProperty, value); }
        }
        public bool IsEntryEnabled
        {
            get { return !(bool)GetValue(IsEntryEnabledProperty); }
            set { SetValue(IsEntryEnabledProperty, !value); }
        }
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public double Maximum
        {
            get { return (double)GetValue(MaxiumumProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public RoundStepper()
        {
            FramePlusBtn = new Frame { Padding=10, HasShadow=true, Margin = 0,VerticalOptions=LayoutOptions.Center };
            FrameMinusBtn = new Frame { Padding =10, HasShadow = true, Margin = 0, VerticalOptions = LayoutOptions.Center };
            FramePlusBtn.SetBinding(Frame.BackgroundColorProperty, new Binding(nameof(StepperColor), BindingMode.OneWay, source: this));
            FramePlusBtn.SetBinding(Frame.HeightRequestProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            FramePlusBtn.SetBinding(Frame.WidthRequestProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            FramePlusBtn.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            FrameMinusBtn.SetBinding(Frame.BackgroundColorProperty, new Binding(nameof(StepperColor), BindingMode.OneWay, source: this));
            FrameMinusBtn.SetBinding(Frame.HeightRequestProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            FrameMinusBtn.SetBinding(Frame.WidthRequestProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            FrameMinusBtn.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(StepperSize), BindingMode.OneWay, source: this));
            PlusBtn = new ImageButton { Source = "symbol_add.png", BackgroundColor=Color.Transparent,Aspect = Aspect.AspectFit };
            FramePlusBtn.Content = PlusBtn;
            MinusBtn = new ImageButton { Source = "symbol_minus.png", BackgroundColor = Color.Transparent, Aspect = Aspect.AspectFit };
            FrameMinusBtn.Content = MinusBtn;

            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.Center;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;

            TextEntry = new Entry { TextColor = Color.Black, VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Keyboard = Keyboard.Numeric, WidthRequest = 40 };
            TextEntry.SetBinding(Entry.FontSizeProperty, new Binding(nameof(FontSize), BindingMode.OneWay, source: this));
            TextEntry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            TextEntry.SetBinding(Entry.IsReadOnlyProperty, new Binding(nameof(IsEntryEnabled), BindingMode.TwoWay, source: this));
            TextEntry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), BindingMode.TwoWay, source: this));
            TextEntry.SetBinding(Entry.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), BindingMode.TwoWay, source: this));
            if (TextEntry.FontSize > (FramePlusBtn.HeightRequest*1.5))
            { TextEntry.FontSize = (FramePlusBtn.HeightRequest * 1.5); }
            TextEntry.TextChanged += Entry_TextChanged;
            Children.Add(FrameMinusBtn);
            Children.Add(TextEntry);
            Children.Add(FramePlusBtn);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
                this.Text = e.NewTextValue;
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Double.TryParse(Text,out double number))
            {
                if (number > Minimum)
                {
                    number -= Increment;
                    Text = number.ToString();
                }
            }      
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if (Double.TryParse(Text, out double number))
            {
                if (number < Maximum)
                {
                    number += Increment;
                    Text = number.ToString();
                }
            }
        }

    }
}
