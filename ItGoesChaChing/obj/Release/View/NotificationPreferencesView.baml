   M S B A M L   `   `   `  ���� I  EItGoesChaChing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ItGoesChaChing.ViewModelItGoesChaChing.ViewModel  5    /ItGoesChaChing.View.NotificationPreferencesView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <	viewModel&clr-namespace:ItGoesChaChing.ViewModel       � DataContext  ?   9ItGoesChaChing.ViewModel.NotificationPreferencesViewModel   � 	Resources  �� MergedDictionaries �� Source��  �� $! ..\Styles\DefaultStyle.xaml?�%    .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� $��Account:���� $	��120������  �� Path$ Accounts� �� SelectedValue �� $ SelectedAccount� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=����� .R��� $ 
Horizontal=�.+��� ���� *UserId+�� $��1��$ 
Horizontal=�.+���    flatButtonStyle#��[  $
��Load������ $ LoadCommand��� #��[  $
��Save������ *SaveCommand+�� $��2��$��Disabled=����� *NotificationPreferences+���� .R�� ���� .R��� 
�������
�������$��1q�
 �� CornerRadius$
 0��$��1 0 1 1q�._��� $ Vertical=�.+�d� |�.  $��30��$��60�� d� 	IsChecked �� *	Enabled+$ TwoWay=����� *	EventType+