   M S B A M L   `   `   `  ���� I  EItGoesChaChing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ItGoesChaChing.ViewModelItGoesChaChing.ViewModel  &     ItGoesChaChing.View.ScheduleView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <	viewModel&clr-namespace:ItGoesChaChing.ViewModel     
������   � DataContext  0   *ItGoesChaChing.ViewModel.ScheduleViewModel   � 	Resources    myStyle �� 
TargetType �� Property �� Value��%u   &	        �� " ��.���  � ��
 � �����  � ��$ Italic$���  � ��$	 8,4q���  � ��$ 4q�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� .+��� $��0��$��Enable Scheduler��$��
10 10 0 10q� d� 	IsChecked �� *Scheduler.Enabled+�� -    ��Name$ weekdayControl��$��1��$��10 0 0 0q����� *Scheduler.Week.Days+���� .R���  �� Orientation$ 
Horizontal=�.+��� $��30������ *Name+�� 	 � IsSharedSizeScope	 . ���� *Hours+���� .R��� $ 
Horizontal=����� .R��� 
���   �$��1q�$��0 0 0 0q�._�� 
 � ColumnDefinitions
 ��  z� SharedSizeGroup$ FirstColumn��$
��Auto ��� $��* ����� " � �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �@J�Opacity$ 1Q�P�  �@J�IsMouseOver .  . ���  �@J�Opacity$	 0.8Q�.+��� $��timeSegment��-   $��20��$��20������ " �� ��  �� Binding �� *	Setting+ �� Value Enabled.a���  � ��
 � � ���  �� *	Setting+ 	PopupOnly.a���  � ��
 ������  �� *	Setting+ Disabled.a���  � ��
 ������ $��2��
 �� $��1* ��� $��1* �.+��� -   $��dayTimeHoverText��$��0��$��	10 10 0 0q��� $��1��.+��� $ 
Horizontal=�$��Center=�$��	10 10 0 0q�.+��� -   $
��test��$��20��$��20��
��� � �
���   �$��2�� J� Opacity$	 0.8Q��� $��Sound and Popup��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��10 5 0 0q�.+��� $��20��$��20��
������
���   �$��2��$	 0.8Q��� $!��Popup only (Sound disabled)��$��10 0 0 0q��� $ 
Horizontal=�$��Center=�$��10 5 0 0q�.+��� $��20��$��20��
�������
���   �$��2��$	 0.8Q��� $
��None��$��10 0 0 0q�