   M S B A M L   `   `   `  ����E  AProdavnica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Prodavnica.FormeProdavnica.Forme  5   	       Prodavnica.Forme.Kupovina   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Prodavnica.Forme     5   	     � Title$  Kupovina��5   	   $	��400��6   $	��580��6'   .��5	      � .+�5
   
   �� ���#212529 �� CornerRadius$ 20��5      � 5   
    � ColumnDefinitions 5      �� 5      $	��228 �6#   �� 5      $	��622 �6#   5      � 5   
   $
��Left=�6   $	��274��6*   .+�5      �� ���#56646b$��0,0,50,0q�5      5      �� 5      $��20,90,69,20q�6   .+�5      �� $��Kupovina��6   $��Center=�6,   $��Center=�6I   
�������$��40(�6w   �� 5      $��20��6   $��30��6'   
������ 63   �� 5      $ ��Uneti ili izmeniti podatke��6   $��Center=�6>   $��Center=�6[   
�������$��15(�6�   �� 5      $��20��6   $	��120��6'   
������ 64   �� 5      -   5        $��CloseButton��5      $��CANCEL��6,   ���#212529
�������$��75��6   5       5!      � 5#   
   $��Right=�6   $	��274��6+   .+�5$      �� $��0,0,0,0q�6   5%      �� 5&      $��
0,10,69,20q�6   .+�5'      �� $��Uneti datum kupovine��6   $
��Left=�68   $��Center=�6S   
�������$��12(�6�   (  "System.Windows.Controls.DatePicker  5(      -   5        $��DPdatum��5(      �� 5)      $��20��6   $��5��6'   
������ 62   �� 5+      $��Uneti iznos kupovine��6   $
��Left=�68   $��Center=�6S   
�������$��12(�6�   �� 5,      -   5        $��txtIznos��5,      $��20��6*   $	��200��66   
�������6W   �� 5-      $��20��6   $��5��6'   
������ 62   �� 5/      $,��&Izabrati način plaćanja: KEŠ/KARICA��6   $
��Left=�6G   $��Center=�6b   
�������$��12(�6�   �� 50      -   5        $��txtNacinPlacanja��50      $��20��62   $	��200��6>   
�������6_   �� 51      $��20��6   $��5��6'   
������ 62   �� 53      $��	ArtikalID��6   $
��Left=�6-   $��Center=�6H   
�������$��12(�6v   �� 54      -   5        $��	cbArtikal��54       �� SelectedValuePath$ 	ArtikalID��6,    �� DisplayMemberPath$ Artikal��6J   �� 55      $��KupacID��6   $
��Left=�6+   $��Center=�6F   
�������$��12(�6t   �� 56      -   5        $��cbKupac��56      $ KupacID��6*   $ Kupac��6F   �� 57      $��OcenaKupcaID��6   $
��Left=�60   $��Center=�6K   
�������$��12(�6y   �� 58      -   5        $��cbOcena��58      $ OcenaKupcaID��6*   $ 
OcenaKupca��6K   �� 59      $��ZaposleniID��6   $
��Left=�6/   $��Center=�6J   
�������$��12(�6x   �� 5:      -   5        $��cbZaposleni��5:      $ ZaposleniID��6.   $ 	Zaposleni��6N   �� 5;      $��20��6   $��15��6'   
������ 63   �� 5=      -	   5        $
��save��5=      $
��SAVE��6%   ���#56646b
�������$��75��6\   6g   5>      5?      5A      5B      