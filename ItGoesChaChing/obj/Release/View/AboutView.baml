   M S B A M L   `   `   `  ���� I  EItGoesChaChing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ItGoesChaChing.ViewModelItGoesChaChing.ViewModel  #    ItGoesChaChing.View.AboutView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     <	viewModel&clr-namespace:ItGoesChaChing.ViewModel       � Title$  It Goes Cha-Ching��$
��Auto��$	��500�� � SizeToContent$ Height=� � Icon$ ..\WindowIcon.ico�� � WindowStyle$
 None=� � AllowsTransparency .  � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=� � DataContext -   'ItGoesChaChing.ViewModel.AboutViewModel  	 � 	Resources	 
 �� MergedDictionaries �� Source�� 
 �� $! ..\Styles\DefaultStyle.xaml?�%    .��� .+��� 
����  �$��1q�._���  �� Orientation$ Vertical=�.+��� $��../Images/Logo.png��$	��200��$	��150���� $ 
Horizontal=�$��Center=�.+��� 
��� $�
Version - �� �� ���  �� Path$ Version� �� Mode$ OneTime=�� $��5q�.+��� $ Vertical=�.+�]� .+��� $��10 0 0 0q�.+��� $��0q����� $ Company�$ OneTime=��� .+���  �� MinLines$ 6�� �� MaxLines$ 6�� �� 
IsReadOnly .  �� TextWrapping$ WrapWithOverflow=�" �� VerticalScrollBarVisibility$ Visible=�
����  �.��-+Your eBay Alerts/Nofifications application.��    flatButtonStyle#��[  $��View Log Files��$��21��$	��220��$��	0 10 0 10q����� $ ViewLogFileClickedCommand�]� .+��� $��0q����� $ 	Copyright�$ OneTime=��� -     largeCloseButtonStyle#��[ 