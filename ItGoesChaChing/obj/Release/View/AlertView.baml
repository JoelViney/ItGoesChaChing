   M S B A M L   `   `   `  ���� I  EItGoesChaChing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ItGoesChaChing.ViewModelItGoesChaChing.ViewModel  9!clr-namespace:ItGoesChaChing.ViewItGoesChaChing.View  E'clr-namespace:ItGoesChaChing.ConvertersItGoesChaChing.Converters  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source7    1ItGoesChaChing.Converters.BitmapToSourceConverter   bitmapToSourceConverter3   -ItGoesChaChing.Converters.AmountTypeConverter  amountTypeConverter  borderBackgroundStyle �� 
TargetType �� Property �� Value  iconImageStyle  timestampTextBlockStyle  headerTextBlockStyle  normalTextBlockStyle  defaultAlertTemplate � ColumnDefinitions � RowDefinitions �� Path �� 	Converter	 �� Mode  toLocalTimeConverter
 �� StringFormat � ToolTip 	 closeButtonStyle �� ElementName �� Orientation !
 listingEventTypesBaseTemplate ! fixedPriceTransactionTemplate �� Triggers �� Value �� Binding N� EnterActions �� Source �� Command �� CommandParameter  askSellerQuestionTemplate  maxLength80Converter �� CornerRadius  invisibleButtonStyle  feedbackReceivedTemplate �� BasedOn �� Triggers ^� 
Conditions �� Binding �� Value  loginTemplate  tickTemplate  exceptionTemplate5   /ItGoesChaChing.Converters.AlertTemplateSelector  alertTemplateSelector   DefaultAlertTemplate   ExceptionTemplate$   ListingEventTypesBaseTemplate    AskSellerQuestionTemplate   FeedbackReceivedTemplate$    FixedPriceTransactionTemplate!   LoginTemplate"   TickTemplate�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     4	viewModel&clr-namespace:ItGoesChaChing.ViewModel  *view!clr-namespace:ItGoesChaChing.View  6
converters'clr-namespace:ItGoesChaChing.Converters    �� $! ..\Styles\DefaultStyle.xaml?�%�  &	        &	      &	 
     &	 �     &	 �     &	 /    &	 |    &	 �    7  7  7   7  7  7  7  7 	 7  &	
     7  7  7   7  7  7  7  7 	 7  7  &	 �    7  7  7   7  7  7  7  7  7 	 7  7  7  7  &	 t
    7  7  7  7  7  7  7 	 7  7  7  7  7  7   7  7  &	 �    7  7  7  7  7  7  7 	 7  7  7  &	 �    7  7  7  7  7  7  7 	 7  &	 W    7  7  7  7 	 7  7  &	 !    7  7  7  7 	 7  &	 �    7  7  7 
 7  7  7  7  7       �� " ��.���  � ��$	 350����  � ��
 �   ���  � ��$ 1q���  � ��$ 0 0 0 1q���  �@��Padding$ 2 2 2 2q���  � ��
 ������� " ��.���  � ��$ 50����  � ��$ 50����  � ��$ 5q��� " ��.���  �@��Padding$ 0 5 0 0q��� " ��.���  � ��$ 	ExtraBold"���  �@��Padding$ 0 5 0 0q��� " ��.���  �@��Padding$ 0 0 0 0q��� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��8�� ���� $ Image�8  �� $��1��$��0��8�� 
��� ��� *
AlertTitle+$	 OneTime=� �� $�(#�� �� ��� *ItemID+$	 OneTime=� �� $�)���� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 F���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��$ Vertical=�.+��� $��This is a default alert.��8�� �� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��8�� ���� $ Image�8  �� $��1��$��0��8�� 
��� ��� *
AlertTitle+$	 OneTime=� �� $�(#�� �� ��� *ItemID+$	 OneTime=� �� $�)���� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 F���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��$ Vertical=�.+��� 8�� ���� *Title+�� 8��	 ���� *CurrentPrice+��  �� $
 true�� �� *DataContext.PlaySound+$
 view�� �� $! ../Data/Sounds/ChaChing.wav?�.R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��8�� ���� $ Item.Media.Image�8   �� *Account.UserId+�� $��1��$��0��8�� 
��� $�Item Sold to�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *Buyer.ViewUserUrl+.�.��� 8�� ���� *Buyer.UserId+�� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 F���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��$ Vertical=�.+��� 8��	 
��� ��� *QuantitySold+ �� $�x�� �� ��� *
AmountPaid+8 
 �� 8�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *Item.ViewItemUrl+.�.��� 8�� ���� *
Item.Title+��  �� $
 true�� �� *DataContext.PlaySound+$
 view�� �� $# ../Data/Sounds/BabyChicks.wav?�.R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��0��$��2��8�� $,��&..\Images\Alerts\AskSellerQuestion.png�� �� *Account.UserId+�� $��1��$��0��8�� 
��� $�Message Received from�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *Sender.ViewUserUrl+.�.��� ���� *Sender.UserId+�� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 f���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��.+��� 8�� ���� *	Subject+8  �� 8��	 
����  ����� *Body+�� $��2��$��Disabled=����� *	MediaList+���� .R�� ���� .R��� 
�������
�������$��1q�$ 0��$��1 0 1 1q�._��� $ Vertical=�.+��� 8��
 ���� *%#DataContext.HyperlinkClickedCommand+$
 view������ *Url+.���� 8�� ���� *Image+8  �� $��1��$��3��$��3��.+��� 8�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *Item.ViewItemUrl+.�.��� 8�� ���� *
Item.Title+�� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2�� �� *Account.UserId+���� " ��8   ��  �� *CommentType+ Positive.a���  � ��$+ %..\Images\Alerts\FeedbackPositive.png����  �� *CommentType+ Neutral.a���  � ��$* $..\Images\Alerts\FeedbackNeutral.png����  �� *CommentType+ Negative.a���  � ��$+ %..\Images\Alerts\FeedbackNegative.png����  �� *CommentType+ IndependentlyWithdrawn.a���  � ��$9 3..\Images\Alerts\FeedbackIndependentlyWithdrawn.png����  �� *CommentType+ 	Withdrawn.a���  � ��$, &..\Images\Alerts\FeedbackWithdrawn.png��^�  ��  �� *CommentType+ Positive��  �� *DataContext.PlaySound+$
 view�� true �� $! ../Data/Sounds/Cheering.wav?�^�  ��  �� *CommentType+ Neutral��  �� *DataContext.PlaySound+$
 view�� true �� $ ../Data/Sounds/Groan.wav?�^�  ��  �� *CommentType+ Negative��  �� *DataContext.PlaySound+$
 view�� true �� $ ../Data/Sounds/Scream.wav?��� $��1��$��0��8�� 
��� $�Feedback Received from�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *CommentingUser.ViewUserUrl+.�.��� ���� *CommentingUser.UserId+�� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 f���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��.+��� 8�� 
��� ��� *CommentText+�� 8�� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *Item.ViewItemUrl+.�.��� 8��	 ���� *
Item.Title+�� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��8�� $ ��..\Images\Alerts\Login.png���� $��1��$��0��8�� 
��� $�Logging in �� ��  �� *%#DataContext.HyperlinkClickedCommand+$
 view�� �� *
AccountUrl+.�.��� ���� *UserId+�� $��2��$��0��8�� ���� *	Timestamp+8  $
 t�� �� *	Timestamp+8  $
 f���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��.+��� 8�� 
��� $�Connecting to eBay...�� �� ��� *Detail+�� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��4��8�� $��..\Images\Alerts\Tick.png���� $��1��$��0��8�� $
��Tick���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��$��0 0 0 5q�.+��� $��This is a debug alert��8�� �� $0��*You can turn this off in the options menu.��8�� �� .R��� 8��  ._��  �� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* �.+��� $��0��$��0��$��4��8�� $��..\Images\Alerts\Tick.png���� $��1��$��0��8�� $��Application Exception���� $��3��$��0��8�� ���� *DataContext.RemoveCommand+$
 view������ �� $��1��$��1��$��3��$��0 0 0 5q�.+��� 8�� ���� *	Message+  8   8  8  8  8  8   8!  8"  