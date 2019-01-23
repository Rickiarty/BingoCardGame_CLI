using System;

/**
 * 
 *  by Rei-Chi Lin @ GitHub 
 * 
 **/

class Solution
{
    const int WIDTH = 5;
    const int HEIGHT = 5;
    const int SEQ_SIZE = 90;
    
    static void Main(string[] args)
    {
        string[] buffer = null;
        
        int N = int.Parse(Console.ReadLine());
        
        int[][][] grids = new int[N][][];
        bool[][][] tables = new bool[N][][];
        int[] sequence = new int[SEQ_SIZE];
        
        for (int i = 0; i < N; i++)
        {
            grids[i] = new int[HEIGHT][];
            tables[i] = new bool[HEIGHT][];
            for(int j = 0; j < HEIGHT; j++) 
            {
                string row = Console.ReadLine();
                buffer = row.Split(' ');
                grids[i][j] = new int[WIDTH];
                tables[i][j] = new bool[WIDTH];
                for(int k = 0; k < WIDTH; k++) 
                {
                    grids[i][j][k] = int.Parse(buffer[k]);
                    tables[i][j][k] = ( j==2 && k==2 )? true : false ;
                }
            }
        }
        string calls = Console.ReadLine();
        buffer = calls.Split(' ');
        for(int i = 0; i < SEQ_SIZE; i++)
        {
            sequence[i] = int.Parse(buffer[i]);
        }
        
        // -------------------------------------- 
        
        int num1 = 0;
        int num2 = 0;
        bool is_used = false;
        int index;
        for(index = 0; index < SEQ_SIZE; index++)
        {
            for(int n = 0; n < N; n++)
            {
                fill_cell_by_checking_num(ref grids[n], sequence[index], ref tables[n]);
                
                if(!is_used && one_line_filled(tables[n]))
                {
                    num1 = index + 1;
                    is_used = true;
                }
                
                if(all_lines_filled(tables[n]))
                {
                    num2 = index + 1;
                    goto LABEL;
                }
            }
        }
        
        LABEL:
        Console.Write("{0}\n{1}\n", num1, num2);
    }
    
    static void fill_cell_by_checking_num(ref int[][] grid, int num, ref bool[][] table)
    {
        for(int y=0; y<HEIGHT; y++)
        {
            for(int x=0; x<WIDTH; x++)
            {
                if(grid[y][x] == num)
                {
                    table[y][x] = true;
                    return;
                }
            }
        }
    }
    
    static int check_diagonals(bool[][] table)
    {
        int result = 0;
        
        bool is_filled = true;
        for(int x=0; x<WIDTH; x++)
        {
            if(!table[x][x])
            {
                is_filled = false;
                break;
            }
        }
        if(is_filled)
        {
            result += 1;
        }
        
        is_filled = true;
        for(int x=0; x<WIDTH; x++)
        {
            if(!table[x][WIDTH-1-x])
            {
                is_filled = false;
                break;
            }
        }
        if(is_filled)
        {
            result += 1;
        }
        
        return result;
    }
    
    static int check_rows(bool[][] table)
    {
        int result = 0;
        
        for(int y=0; y<HEIGHT; y++)
        {
            bool is_filled = true;
            for(int x=0; x<WIDTH; x++)
            {
                if(!table[y][x])
                {
                    is_filled = false;
                    break;
                }
            }
            if(is_filled)
            {
                result += 1;
            }
        }
        
        return result;
    }
    
    static int check_columns(bool[][] table)
    {
        int result = 0;
        
        for(int x=0; x<WIDTH; x++)
        {
            bool is_filled = true;
            for(int y=0; y<HEIGHT; y++)
            {
                if(!table[y][x])
                {
                    is_filled = false;
                    break;
                }
            }
            if(is_filled)
            {
                result += 1;
            }
        }
        
        return result;
    }
    
    static bool one_line_filled(bool[][] table)
    {
        bool result = false;
        
        if(check_diagonals(table) > 0)
        {
            result = true;
            return result;
        }
        
        if(check_columns(table) > 0)
        {
            result = true;
            return result;
        }
        
        if(check_rows(table) > 0)
        {
            result = true;
            return result;
        }
        
        return result;
    }
    
    static bool all_lines_filled(bool[][] table)
    {
        bool result = true;
        
        for(int y=0; y<HEIGHT; y++)
        {
            for(int x=0; x<WIDTH; x++)
            {
                if(!table[y][x])
                {
                    result = false;
                    goto label1;
                }
            }
        }
        
        label1:
        return result;
    }
    
}

/*

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#define byte char
#define bool char
#define true 1
#define false 0

#define WIDTH 5
#define HEIGHT 5
#define SEQ_SIZE 90

void fill_cell_by_checking_num(byte**, int, bool**);
int check_diagonals(bool**);
int check_rows(bool**);
int check_columns(bool**);
bool one_line_filled(bool**);
bool all_lines_filled(bool**);

int main()
{
    int N;
    int temp;
    byte grids[N][HEIGHT][WIDTH];
    bool tables[N][HEIGHT][WIDTH];
    byte sequence[SEQ_SIZE];
    
    scanf("%d", &N); getc(stdin);
    for(int i = 0; i < N; i++) 
    {
        for(int j = 0; j < HEIGHT; j++)
        {
            for(int k = 0; k < WIDTH; k++)
            {
                scanf("%d", &temp); getc(stdin);
                //*(*(*(grids+i*WIDTH*HEIGHT)+j*WIDTH)+k) = (byte)temp;
                grids[i][j][k] = (byte)temp;
                tables[i][j][k] = ( j==2 && k==2 )? true : false ;
            }
        }
    }
    for(int i = 0; i < SEQ_SIZE; i++) 
    {
        scanf("%d", &temp); getc(stdin);
        *(sequence+i) = (byte)temp;
    }
    
    // ----------------------------------- 
    
    int num1 = 0;
    int num2 = 0;
    bool is_used = false;
    int index;
    for(index = 0; index < SEQ_SIZE; index++)
    {
        for(int n = 0; n < N; n++)
        {
            fill_cell_by_checking_num(grids[n], sequence[index], tables[n]);
            
            if(!is_used && one_line_filled(tables[n]))
            {
                num1 = index + 1;
                is_used = true;
            }
            
            if(all_lines_filled(tables[n]))
            {
                num2 = index + 1;
                goto LABEL;
            }
        }
    }
    
    LABEL:
    printf("%d\n%d\n", num1, num2);

    return 0;
}

void fill_cell_by_checking_num(byte** grid, int num, bool** table)
{
    for(int y=0; y<HEIGHT; y++)
    {
        for(int x=0; x<WIDTH; x++)
        {
            if(*(*(grid+y*WIDTH)+x) == num)
            {
                *(*(table+y*WIDTH)+x) = true;
                return;
            }
        }
    }
}

int check_diagonals(bool** table)
{
    int result = 0;
    
    bool is_filled = true;
    for(int x=0; x<WIDTH; x++)
    {
        if(!*(*(table+x*WIDTH)+x))
        {
            is_filled = false;
            break;
        }
    }
    if(is_filled)
    {
        result += 1;
    }
    
    is_filled = true;
    for(int x=0; x<WIDTH; x++)
    {
        if(!*(*(table+x*WIDTH)+WIDTH-1-x))
        {
            is_filled = false;
            break;
        }
    }
    if(is_filled)
    {
        result += 1;
    }
    
    return result;
}

int check_rows(bool** table)
{
    int result = 0;
    
    for(int y=0; y<HEIGHT; y++)
    {
        bool is_filled = true;
        for(int x=0; x<WIDTH; x++)
        {
            if(!*(*(table+y*WIDTH)+x))
            {
                is_filled = false;
                break;
            }
        }
        if(is_filled)
        {
            result += 1;
        }
    }
    
    return result;
}

int check_columns(bool** table)
{
    int result = 0;
    
    for(int x=0; x<WIDTH; x++)
    {
        bool is_filled = true;
        for(int y=0; y<HEIGHT; y++)
        {
            if(!*(*(table+y*WIDTH)+x))
            {
                is_filled = false;
                break;
            }
        }
        if(is_filled)
        {
            result += 1;
        }
    }
    
    return result;
}

bool one_line_filled(bool** table)
{
    bool result = false;
    
    if(check_diagonals(table) > 0)
    {
        result = true;
        goto label2;
    }
    
    if(check_columns(table) > 0)
    {
        result = true;
        goto label2;
    }
    
    if(check_rows(table) > 0)
    {
        result = true;
        goto label2;
    }
    
    label2:
    return result;
}

bool all_lines_filled(bool** table)
{
    bool result = true;
    
    for(int y=0; y<HEIGHT; y++)
    {
        for(int x=0; x<WIDTH; x++)
        {
            if(!*(*(table+y*WIDTH)+x))
            {
                result = false;
                goto label1;
            }
        }
    }
    
    label1:
    return result;
}

*/