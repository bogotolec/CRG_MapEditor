   M S B A M L   `   `   `  ����D  @MapEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MapEditor	MapEditor  5   	       MapEditor.MapCreate   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MapEditor     5   	     � Title$  New map��5   	   $	��200��6   $	��200��6&   $	��200��5	   	   $	��200��6   $	��200��5
   	   $	��200��6    � 
ResizeMode$ NoResize=�5   	   .��5      � .+�5   
   �� -   5         ��Name$ AskingForHeight��5      $��Height: ��6)   $
��Left=�6<   $��	25,25,0,0q�6W   $	��Top=�6j   �� 5   
   -   5        $ AskingForWidth��5      $��Width: ��6(   $
��Left=�6:   $��	25,50,0,0q�6U   $	��Top=�6h   �� 5   
   -   5        $ AskingForLandscape��5      $��Landscape: ��6,   $
��Left=�6B   $��	10,75,0,0q�6]   $	��Top=�6p   �� 5   
   -   5         ��Name$ HeightField��5      $
��Left=�6'   $��25��6B   $��	80,25,0,0q�6N    �� TextWrapping$ NoWrap=�6a   $
��1024��6w   $	��Top=�6�   $	��100��6�   $��16(�6�   �� 5   
   -   5        $ 
WidthField��5      $
��Left=�6&   $��25��6A   $��	80,50,0,0q�6M   $ NoWrap=�6`   $
��1024��6v   $	��Top=�6�   $	��100��6�   $��16(�6�   �� 5   
   -   5         ��Name$ 	Landscape��5      $
��Left=�6&   $��25��6A   $��	80,75,0,0q�6M   $	��Top=�6`   $	��100��6x   .@�5      �� Field6   �� 5      Desert6    �� 5      Water6   �� 5      Forest6    �� 5      Bricks6    �� 5      Lava6   �� 5      Sign6   5      �� 5   
   -   5         ��Name$ Create��5      $��Create��67   $
��Left=�6H   $��
65,120,0,0q�6c   $	��Top=�6w   $��70��6�    J� RenderTransformOrigin$ 
0.787,0.55&�6�   5      5      