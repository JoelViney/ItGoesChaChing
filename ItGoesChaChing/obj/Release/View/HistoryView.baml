   M S B A M L   `   `   `  ���� I  EItGoesChaChing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ItGoesChaChing.ViewModelItGoesChaChing.ViewModel  E'clr-namespace:ItGoesChaChing.ConvertersItGoesChaChing.Converters  %    ItGoesChaChing.View.HistoryView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     <	viewModel&clr-namespace:ItGoesChaChing.ViewModel     >
converters'clr-namespace:ItGoesChaChing.Converters     -   $
��view��  � SizeToContent$  Width=�$	��400��$	��200�� � Icon$ ..\WindowIcon.ico�� � WindowStyle$
 None=� � AllowsTransparency .  � 
ResizeMode$ CanResizeWithGrip=� � ShowInTaskbar .  � WindowStartupLocation$ CenterScreen=�
������� � Title �� *ApplicationTitle+ � DataContext /   )ItGoesChaChing.ViewModel.HistoryViewModel  	 � 	Resources	 
 �� MergedDictionaries �� Source�� 
 �� $! ..\Styles\DefaultStyle.xaml?��� $ ..\View\AlertView.xaml?�%    .���� $��1��
����  �$��1q�$��0q�._��  � RowDefinitions �� $
��Auto ��� $��* ��� $��15 �.+�� -   $��0��
�������$��21��$	��Top=�$��0 0 0 0q�.+��� $��It Goes Cha-Ching��
����  �$��5 2 0 0q�$
��Bold"��� -      largeCloseButtonStyle#��[  �� $��1��$
��Auto=�.����   alertTemplateSelector#��[ ����  �� Path$ Alerts�