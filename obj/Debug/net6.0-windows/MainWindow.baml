   M S B A M L   `   `   `  ����L  HNBA_Player_Awards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:NBA_Player_AwardsNBA_Player_Awards  5   	   "    NBA_Player_Awards.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:NBA_Player_Awards     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��810��6)   .��5	      �� $
��True��6   $
��Auto=�6+   $
��Auto=�6P   .��5   
   � $	��810��6   .+�5      �� $��NBA Player Awards��6   $��Center=�60   $��0,45,0,0q�6M   $	��Top=�6_   $��30(�6w   �� 5      $��Prius Award��6   $��
20,120,0,0q�6*   $	��Top=�6>   $
��Bold"�6V   $��Italic$�6h   �� 5      -   5        $��
priusAward��5      $��20,125,462,0q�6*   $��noWrap=�6@   $��Player Name��6V    �� TextAlignment$ Right=�6i   $	��Top=�6   �� 5      $��Gas Guzzler Award��6   $��20,150,738,0q�60   $	��Top=�6F   $��72��6^   $
��Bold"�6i   $��Italic$�6{   �� 5      -   5        $��gasGuzzlerAward��5      $��20,155,462,0q�6/   $��noWrap=�6E   $��Player Name��6[   $	��Top=�6n   $ Right=�6�   �� 5      $��Foul Target Award��6   $��
20,180,0,0q�60   $	��Top=�6D   $
��Bold"�6\   $��Italic$�6n   �� 5      -   5        $��foulTargetAward��5      $��190,185,424,0q�6/   $��noWrap=�6F   $��Player Name��6\   $	��Top=�6o   $	��120��6�   $ Right=�6�   $��Center=�6�   �� 5      $ ��Over Achiever Award: Top 3��6   $��
20,210,0,0q�69   $	��Top=�6M   $
��Bold"�6e   $��Italic$�6w   �� 5      -   5         ��Name$ overachiever1��5      $��228,215,0,0q�6/   $��noWrap=�6D   $��Player Name��6Z   $	��Top=�6m   $	��120��6�   $ Right=�6�   $
��Left=�6�   �� 5      -   5        $ overachiever2��5      $��228,231,0,0q�6/   $��noWrap=�6D   $��Player Name��6Z   $	��Top=�6m   $	��120��6�   $ Right=�6�   $
��Left=�6�   �� 5      -   5        $ overachiever3��5      $��228,247,0,0q�6/   $��noWrap=�6D   $��Player Name��6Z   $	��Top=�6m   $	��120��6�   $ Right=�6�   $
��Left=�6�   �� 5      $,��&Under Achiever Award: Bottom 3 Players��6   $��415,210,0,0q�6E   $	��Top=�6Z   $
��Bold"�6r   $��Italic$�6�   �� 5      -   5        $��underachiever1��5      $��
0,215,20,0q�6.   $��noWrap=�6B   $��Player Name��6X   $	��Top=�6k   $	��375��6�   $ Right=�6�   $��Right=�6�   �� 5      -   5        $��underachiever2��5      $��
0,231,20,0q�6.   $��noWrap=�6B   $��Player Name��6X   $	��Top=�6k   $	��375��6�   $ Right=�6�   $��Right=�6�   �� 5      -	   5        $��underachiever3��5      $��
0,247,20,0q�6.   $��noWrap=�6B   $��Player Name��6X   $	��Top=�6k   $	��375��6�   $ Right=�6�   $��Right=�6�   �� 5      $��On the Fence Award��6   $��415,120,0,0q�61   $	��Top=�6F   $
��Bold"�6^   $��Italic$�6p   �� 5      -
   5        $��onTheFenceAward��5      $��670,125,20,0q�6/   $��noWrap=�6E   $��Player Name��6[   $	��Top=�6n   $ Right=�6�   $	��120��6�   $��Right=�6�   �� 5      $��Bang For Your Buck Award��6   $��415,150,0,0q�67   $	��Top=�6L   $
��Bold"�6d   $��Italic$�6v   �� 5      -   5        $��bangBuckAward��5      $��415,155,20,0q�6-   $��NoWrap=�6C   $��Player Name��6Y   $	��Top=�6l   $ Right=�6�   $	��375��6�   �� 5      $��Gordon Gekko Award��6   $��415,180,0,0q�61   $	��Top=�6F   $
��Bold"�6^   $��Italic$�6p   �� 5       -   5        $��gordonGekkoAward��5       $��
0,185,20,0q�60   $��noWrap=�6D   $��Player Name��6Z   $	��Top=�6m   $ Right=�6�   $	��375��6�   $��Right=�6�   �� 5!      -   5        $'��!Charlie Brown-TigerUpercut Awards��5!      $��Center=�6A   $��	0,275,0,0q�6^   $	��Top=�6q   6�   5"      5#      5$      