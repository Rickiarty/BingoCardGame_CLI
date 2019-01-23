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
