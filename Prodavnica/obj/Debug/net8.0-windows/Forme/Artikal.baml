   M S B A M L   `   `   `  ����E  AProdavnica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Prodavnica.FormeProdavnica.Forme  5   	       Prodavnica.Forme.Artikal   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Prodavnica.Forme     5   	     � Title$  Artikal��5   	   $	��400��6   $	��580��6&   .��5	      � .+�5
   
   �� ���#212529 �� CornerRadius$ 20��5   	   5      � 5   
    � ColumnDefinitions 5      �� 5      $	��228 �6#   �� 5      $	��622 �6#   5      � 5   
   $
��Left=�6   $	��274��6*   .+�5      �� ���#56646b$��0,0,50,0q�6*   5      �� 5      $��20,90,69,20q�6   .+�5      �� $��Artikal��6   $��Center=�6+   $��Center=�6H   
�������$��60(�6v   �� 5      $��20��6   $��30��6'   
������ 63   �� 5      $ ��Uneti ili izmeniti podatke��6   $��Center=�6>   $��Center=�6[   
�������$��15(�6�   �� 5      $��20��6   $��90��6'   
������ 63   �� 5      -   5        $��CloseButton��5      $��CANCEL��6,   ���#212529
�������$��75��6   5      5       � 5"   
   $��Right=�6   $	��274��6+   .+�5#      �� �� 5$      $��20,10,43,-32q�6   .+�5%      �� $��Uneti naziv artikla��6   $
��Left=�67   $��Center=�6R   
�������$��12(�6�   �� 5&      -   5        $��txtNaziv��5&      $��30��6*   $	��205��66   
�������6X   �� 5(      $��Uneti veličinu artikla��6   $
��Left=�6:   $��Center=�6U   
�������$��12(�6�   �� 5)      -   5        $��txtVelicina��5)      $��30��6-   $	��207��69   
�������6Z   �� 5+      $��Uneti cenu artikla��6   $
��Left=�66   $��Center=�6Q   
�������$��12(�6   �� 5,      -   5        $��txtCena��5,      $��30��6)   $	��209��65   
�������6U   �� 5-      $��20��6   $��5��6'   
������ 62   �� 5/      $*��$Uneti raspoloživu količinu artikla��6   $
��Left=�6F   $��Center=�6a   
�������$��12(�6�   �� 50      -   5        $��txtRaspolozivaKolicina��50      $��30��68   $	��223��6D   
�������6d   �� 51      $��KategorijaID��6   $
��Left=�60   $��Center=�6K   
�������$��12(�6y   �� 52      -   5        $��cbKategorija��52       �� SelectedValuePath$ KategorijaID��6/    �� DisplayMemberPath$ 
Kategorija��6P   �� 53      $��MarkaID��6   $
��Left=�6+   $��Center=�6F   
�������$��12(�6t   �� 54      -   5        $��cbMarka��54      $ MarkaID��6*   $ Marka��6F   �� 55      $��OcenaKupcaID��6   $
��Left=�60   $��Center=�6K   
�������$��12(�6y   �� 56      -   5        $��cbOcenaKupca��56      $ OcenaKupcaID��6/   $ 
OcenaKupca��6P   �� 57      $��20��6   $��5��6'   
������ 62   �� 58      -	   5        $
��save��58      $
��SAVE��6%   ���#56646b
�������$��75��6\   6g   59      5:      5<      5=      