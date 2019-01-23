#include "BingoCardGame.h"

/**
 * 
 *  by Rei-Chi Lin @ GitHub 
 * 
 **/

#define WIDTH 5
#define HEIGHT 5
#define SEQ_SIZE 90

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
                tables[i][j][k] = ( j == HEIGHT / 2 && k == WIDTH / 2 )? true : false ;
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

bool fill_cell_by_checking_num(byte** grid, int num, bool** table)
{
    for(int y=0; y<HEIGHT; y++)
    {
        for(int x=0; x<WIDTH; x++)
        {
            if(*(*(grid+y*WIDTH)+x) == num)
            {
                *(*(table+y*WIDTH)+x) = true;
                return true;
            }
        }
    }
    return false;
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
