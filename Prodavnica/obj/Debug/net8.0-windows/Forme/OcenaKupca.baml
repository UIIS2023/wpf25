   M S B A M L   `   `   `  ����E  AProdavnica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Prodavnica.FormeProdavnica.Forme  5   	   !    Prodavnica.Forme.OcenaKupca   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Prodavnica.Forme     5   	     � Title$  
OcenaKupca��5   	   $	��400��6   $	��580��6)   .��5	      � .+�5
   
   �� ���#212529 �� CornerRadius$ 20��5      � 5   
    � ColumnDefinitions 5      �� 5      $	��228 �6#   �� 5      $	��622 �6#   5      � 5   
   $
��Left=�6   $	��274��6*   .+�5      �� ���#56646b$��0,0,50,0q�5      5      �� 5      $��20,90,69,20q�6   .+�5      �� $��Ocena kupca��6   $��Center=�6/   $��Center=�6L   
�������$��30(�6z   �� 5      $��20��6   $��30��6'   
������ 63   �� 5      $ ��Uneti ili izmeniti podatke��6   $��Center=�6>   $��Center=�6[   
�������$��15(�6�   �� 5      $��20��6   $	��130��6'   
������ 64   �� 5      -   5        $��CloseButton��5      $��CANCEL��6,   ���#212529
�������$��75��6   5       5!      � 5#   
   $��Right=�6   $	��274��6+   .+�5$      �� $��0,0,0,0q�6   5%      �� 5&      $��20,10,69,20q�6   .+�5'      �� $��Uneti ocenu od 1 do 5��6   $
��Left=�69   $��Center=�6T   
�������$��12(�6�   �� 5(      -   5        $��txtOcena��5(      $��30��6*   $	��200��66   
�������6X   �� 5)      $��20��6   $��10��6'   
������ 63   �� 5+      $��Uneti komentar��6   $
��Left=�62   $��Center=�6M   
�������$��12(�6{   �� 5,      -   5        $��txtKomentar��5,      $��30��6-   $	��200��69   
�������6[   �� 5-      $��20��6   $��10��6'   
������ 63   �� 5/      $��Uneti datum ocene��6   $
��Left=�65   $��Center=�6P   
�������$��12(�6~   (  "System.Windows.Controls.DatePicker  50      -   5        $��DPdatum��50      �� 51      $��20��6   $��10��6'   
������ 63   �� 52      $��KupacID��6   $
��Left=�6+   $��Center=�6F   
�������$��12(�6t   �� 53      -   5        $��cbKupac��53       �� SelectedValuePath$ KupacID��6*    �� DisplayMemberPath$ KupacID��6F   �� 54      $��
KupovinaID��6   $
��Left=�6.   $��Center=�6I   
�������$��12(�6w   �� 55      -   5        $��
cbKupovina��55      $ 
KupovinaID��6-   $ 
KupovinaID��6L   �� 57      $��20��6   $��58��6'   
������ 63   �� 58      -   5        $
��save��58      $
��SAVE��6%   ���#56646b
�������$��75��6\   6g   59      5:      5<      5=      