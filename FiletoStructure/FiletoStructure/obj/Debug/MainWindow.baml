   M S B A M L   `   `   `  ����J  FFiletoStructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:FiletoStructureFiletoStructure  5   	        FiletoStructure.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:FiletoStructure     5   	     � Title$   Create Directory Structure��5   	   $��280.771��6,   $��531.835��6=   .��5	      � .+�5
   
   �� -   5         ��Name$ txtbxDir��5
      $
��Left=�6$   $��23��6?   $��	22,52,0,0q�6K   $	��Top=�6^   $	��484��6v   6�   �� 5   
   $��Top Level Folder Location��6   $
��Left=�64   $��	21,21,0,0q�6O   $	��Top=�6b   �� 5   
   -   5         ��Name$ btnDir��5      $��Browse��6!   $
��Left=�62   $��
431,24,0,0q�6M   $	��Top=�6a   $��75��6y   6�   �� 5   
   -   5        $ 	txtbxFile��5      $
��Left=�6%   $��23��6@   $��
22,111,0,0q�6L   $	��Top=�6`   $	��484��6x   6�   �� 5   
   $��Select Directory Outline��6   $
��Left=�63   $��	22,80,0,0q�6N   $	��Top=�6a   �� 5   
   -   5        $ btnFile��5      $��Browse��6"   $
��Left=�63   $��
431,83,0,0q�6N   $	��Top=�6b   $��75��6z   6�   �� 5   
   -   5        $ 
btnMakeDir��5      $��Make Directories��6%   $
��Left=�6@   $��
22,221,0,0q�6[   $	��Top=�6o   $	��108��6�   {�.  6�   �� 5   
   -   5         ��Name$ 	cbReplace��5      $�� ��6&   $
��Left=�61   $��
28,195,0,0q�6L   $	��Top=�6`   $y�Hidden=�6x   6�   �� 5   
   -   5         ��Name$ 
lblReplace��5      $
��Left=�6(   $��
58,194,0,0q�6C   $
��Wrap=�6W   $0��*Replace invalid characters with underscore��6k   $	��Top=�6�   $y�Hidden=�6�   �� 5   
   -   5        $ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��431,221,0,0q�6P   $	��Top=�6e   $��75��6}   6�   �� 5   
   -	   5        $ lblPath��5      $
��Left=�6%   $��180,223,0,0q�6@   $
��Wrap=�6U   $,��&Longest new Path is 0 characters long.��6i   $	��Top=�6�   �� 5   
   -
   5         ��Name$ 
imgWarning��5      $
��Left=�6$   $��32��6?   $��
21,139,0,0q�6K   $	��Top=�6_   $��32��6w   $y�Hidden=�6�   �� 5   
   -   5         ��Name$ scrError��5      $
��Left=�6)   $��50��6D   $��
58,139,0,0q�6P   $	��Top=�6d   $	��448��6|   
���   �$��0q�6�   $y�Hidden=�6�   �� 5   
   -   5        $ 	btnShowMe��5      $��Show Me��6$   $
��Left=�66   $��431,192,0,0q�6Q   $	��Top=�6f   $��75��6~   $y�Hidden=�6�   6�   5      5      