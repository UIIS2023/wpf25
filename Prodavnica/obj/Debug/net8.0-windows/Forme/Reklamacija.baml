   M S B A M L   `   `   `  ����E  AProdavnica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Prodavnica.FormeProdavnica.Forme  5   	   "    Prodavnica.Forme.Reklamacija   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Prodavnica.Forme     5   	     � Title$  Reklamacija��5   	   $	��400��6   $	��580��6*   .��5	      � .+�5
   
   �� ���#212529 �� CornerRadius$ 20��5      � 5   
    � ColumnDefinitions 5      �� 5      $	��228 �6#   �� 5      $	��622 �6#   5      � 5   
   $
��Left=�6   $	��274��6*   .+�5      �� ���#56646b$��0,0,50,0q�5      5      �� 5      $��20,90,69,20q�6   .+�5      �� $��Reklamacija��6   $��Center=�6/   $��Center=�6L   
�������$��35(�6z   �� 5      $��20��6   $��30��6'   
������ 63   �� 5      $ ��Uneti ili izmeniti podatke��6   $��Center=�6>   $��Center=�6[   
�������$��15(�6�   �� 5      $��20��6   $	��120��6'   
������ 64   �� 5      -   5        $��CloseButton��5      $��CANCEL��6,   ���#212529
�������$��75��6   $��21��6�   5      5       � 5"   
   $��Right=�6   $	��274��6+   .+�5#      �� $��0,0,0,0q�6   5$      �� 5%      $��
0,10,69,20q�6   .+�5&      �� $��Uneti razlog reklamacije��6   $
��Left=�6<   $��Center=�6W   
�������$��12(�6�   �� 5'      -   5        $��	txtRazlog��5'      $��70��6+   $	��200��67   
�������6Y   �� 5)      $��Uneti broj računa��6   $
��Left=�65   $��Center=�6P   
�������$��12(�6~   �� 5*      -   5        $��txtBrojRacuna��5*      $��20��6/   $	��200��6;   
�������6]   �� 5,      $��Uneti datum reklamacije��6   $
��Left=�6;   $��Center=�6V   
�������$��12(�6�   (  "System.Windows.Controls.DatePicker  5-      -   5        $��DPdatum��5-      �� 5/      $#��Reklamacija ODOBRENA/ODBIJENA��6   $
��Left=�6A   $��Center=�6\   
�������$��12(�6�   �� 50      -   5        $��	txtStatus��50      $��20��6+   $	��200��67   
�������6Y   �� 51      $��	ArtikalID��6   $
��Left=�6-   $��Center=�6H   
�������$��12(�6v   �� 52      -   5        $��	cbArtikal��52       �� SelectedValuePath$ 	ArtikalID��6,    �� DisplayMemberPath$ 	ArtikalID��6J   �� 53      $��ZaposleniID��6   $
��Left=�6/   $��Center=�6J   
�������$��12(�6x   �� 54      -   5        $��cbZaposleni��54      $ ZaposleniID��6.   $ ZaposleniID��6N   �� 55      $��KupacID��6   $
��Left=�6+   $��Center=�6F   
�������$��12(�6t   �� 56      -   5        $��cbKupac��56      $ KupacID��6*   $ KupacID��6F   �� 57      $��20��6   $��5��6'   
������ 62   �� 59      -	   5        $
��save��59      $
��SAVE��6%   ���#56646b
�������$��75��6\   6g   5:      5;      5=      5>      