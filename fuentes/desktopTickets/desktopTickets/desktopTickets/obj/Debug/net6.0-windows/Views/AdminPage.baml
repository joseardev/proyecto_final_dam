   M S B A M L   `   `   `  ����  desktopTickets$    desktopTickets.Views.AdminPage   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Usuarios��5   	   $	��700��6   $	��450��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Usuario:��6   $
��Left=�60   �� 5    
   $��0��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6d   ��6G   �� *NewUsername+6d   �� 5#   
   $��1��6   $��Nombre:��6   $
��Left=�6/   �� 5$   
   $��1��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6b   ��6G   �� *	NewNombre+6b   �� 5'   
   $��2��6   $��
Apellidos:��6   $
��Left=�62   �� 5(   
   $��2��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6e   ��6G   �� *NewApellidos+6e   �� 5+   
   $��3��6   $��Centro:��6   $
��Left=�6/   �� 5,   
   $��3��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6b   ��6G   �� *	NewCentro+6b   �� 5/   
   $��4��6   $��Contraseña:��6   $
��Left=�63   �� 50   
   $��4��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6d   ��6G   �� *NewPassword+6d   �� 53   
   $��5��6   $��Perfil:��6   $
��Left=�6/   �� 54   
   $��5��6   $��1��6    $	��300��60   $��10q�6<   $��Right=�6�   ��6H   �� *
Perfiles+ �� SelectedItem 6i   �� *	NewPerfil+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 57   
   $��6��6   $��Correo Electrónico:��6   $
��Left=�6;   �� 58   
   $��6��6   $��1��6   $	��300��6/   $��10q�6;   $��Right=�6`   ��6G   �� *	NewMail+6`   �� 5;   
   $��7��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $	��Top=�6h   .+�5<      �� $��Crear TK��6   $��5q�6V    d� 	IsChecked 6*   �� *
NewCrear+$ TwoWay=�6V   �� 5=      $��Modificar TK��6   $��5q�6^    6.   �� *NewModificar+$ TwoWay=�6^   �� 5>      $��	Borrar TK��6   $��5q�6X    6+   �� *	NewDelete+$ TwoWay=�6X   5?      �� 5B   
   $��8��6   $��1��6   $	��200��6.   $��10q�6:   $��Agregar Usuario��6F   $��Right=�6�   ��6`   �� *AddUserCommand+6�   �� 5C   
   $��9��6   $��1��6   $	��200��6.   $��10q�6:   $��Actualizar Usuario��6F   $��Right=�6�   ��6c   �� *UpdateUserCommand+6�   �� 5D   
   $��10��6   $��1��6   $	��200��6/   $��10q�6;   $��Eliminar Usuario��6G   $��Right=�6�   ��6b   �� *DeleteUserCommand+6�   &   System.Windows.Controls.DataGrid  5G   
   $��11��6   $��2��6!      AutoGenerateColumns$ False��6�   ��65   �� *PersonalList+ 6Z   �� *SelectedUser+	   Columns	 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Usuario��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6R      
IsReadOnly$
 True��6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *	USUARIO+6_     5J      $
 Nombre��6%   $
 Auto 6P   $
 True��6]    65   �� *NOMBRE+6]     5K      $
 	Apellidos��6%   $
 Auto 6V   $
 True��6c    68   �� *	APELLIDOS+6c     5L      $
 Centro��6%   $
 Auto 6P   $
 True��6]    65   �� *CENTRO+6]     5M      $
 Contraseña��6%   $
 Auto 6V   $
 True��6c    69   �� *
PASSWORD+6c     5N      $
 Perfil��6%   $
 Auto 6P   $
 True��6]    65   �� *PERFIL+6]   5Q      5R      5S      