/**
 * 
 *  by Rei-Chi Lin @ GitHub 
 * 
 **/

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define byte char
#define bool char
#define true 1
#define false 0

void fill_cell_by_checking_num(byte**, int, bool**);
int check_diagonals(bool**);
int check_rows(bool**);
int check_columns(bool**);
bool one_line_filled(bool**);
bool all_lines_filled(bool**);
