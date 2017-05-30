## Main Window

For PTR Viewer Manual go [here](PTR_Viewer_Manual.md).

### Common problems

* [Style](#style-problems)

#### Style Problems <a name="style-problems"/>

While writing controls with style is esay to bump into a problem with triggers. They are firing but nothing changes. It is a good habbit to check whether they are not overriden by set properties. All properties that are available in xaml are DependencyProperties. They have [setting precedence][1], it is so that you can set them in many ways and some of them have higher priority, which leads to an easy mistake to override its value. Let's look at an example:

```xaml
<Button Background="Red">  
	<Button.Style>  
		<Style TargetType="{x:Type Button}">  
			<Setter Property="Background" Value="Blue"/>  
			<Style.Triggers>  
				<Trigger Property="IsMouseOver" Value="True">  
					<Setter Property="Background" Value="Green"/>  
				</Trigger>  
			</Style.Triggers>
		</Style>  
	</Button.Style>  
</Button>
```

In this example we could want to color the button in green on trigger, but instead it will be red. It is because local value is higher in priority over trigger and style value. If you want trigger to work. Set things in style and override them by triggers. It is a very common mistake, when you first make a control with locally set properties, and then decide to make a trigger in a style, and you get frustrated when you see no changes. And this is why it happens.



[1]:https://msdn.microsoft.com/en-us/library/ms743230(v=vs.110).aspx