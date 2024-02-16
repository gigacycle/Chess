using System;

namespace Chess
{
	/// <summary>
	/// Summary description for Stone.
	/// </summary>
	
	#region This is the IStone interface
	public interface IStone
	{
		int move(Board b, int x2, int y2);
		int playable(Board b, int x2, int y2);
	}
	#endregion

	#region This is the Stone class
	public class Stone : System.Object
	{
		public Stone()
		{
			color = 1;
			x = 1;
			y = 1;
		}
		public int color;
		public int x;
		public int y;
	}
	#endregion

	#region This is the class file for pawn
	public class Pawn : Stone, IStone
	{
		public Pawn(int c, int i, int j)
		{
			color = c;
			x = i;
			y = j;
		}
		public int move(Board brd, int x2,int y2)
		{
			int k = brd.getInfo(x2, y2);
			int m,n=0;
			
			if(color == 2)
				m=-1;
			
			else
				m=1;
			
			if((y==6&&color==2)||(y==1&&color==1))
				n=1;
			
			if((k == 0) && (x == x2) && ((y2==y+m)||(n==1&&(y2==(y+2*m) && (brd.getInfo(x2,y+m)==0)))))
			{
				return 1;
			}

			if (color == 1 && k > 6 && (x2 == (x + 1) || x2 ==(x - 1)) && y2 == (y + 1))
			{
				return 1;
			}
			if (color == 2 && k != 0 && k < 7 && (x2 == (x - 1) || x2 == (x + 1)) && y2 == (y - 1))
			{
				return 1;
			}
			return 0;
		}
		public int playable(Board brd, int x2, int y2)
		{
			Point p=brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(6,x2,y2);
				else
					brd2.setSquare(12,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else
				return 1;
		}	
	}
	#endregion

	#region This is the class file for Knight
	public class Knight: Stone, IStone
	{
		public Knight(int c, int i, int j)
		{
			color = c;
			x = i;
			y = j;
		}
		public int move(Board brd, int x2,int y2)
		{
			int k=brd.getInfo(x2, y2);
			int v1=x-x2;
			int v2=y-y2;
			
			if (v1<0)
				v1=-v1;
			
			if (v2<0)
				v2=-v2;
			
			if((k == 0) && ((v1==2&&v2==1) || (v2==2&&v1==1)))
			{
				return 1;
			}

			if (color == 1 && k > 6 && ((v1==2&&v2==1)||(v2==2&&v1==1)))
			{
				return 1;	
			}

			if (color == 2 && k < 7 && ((v1==2&&v2==1)||(v2==2&&v1==1)))
			{
				return 1;	
			}
			return 0;
		}
		public int playable(Board brd, int x2, int y2)
		{
			Point p=brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(2,x2,y2);
				else
					brd2.setSquare(8,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else 
				return 1;
		}	
	}
	#endregion

	public class Castle : Stone, IStone
	{
		public Castle(int c, int i, int j)
		{
			color=c;
			x=i;
			y=j;
		}
		public int move(Board brd, int x2, int y2)
		{
			int k = brd.getInfo(x2, y2);
			int i;
			if(x == x2)
			{
				if(y < y2)
				{
					for(i = y + 1; i < y2; i++ )
					{
						if(brd.getInfo(x, i) !=0)
							return 0;
					}
				}

				if(y > y2)
				{
					for(i = y - 1; i > y2; i-- )
					{
						if(brd.getInfo(x, i) !=0)
							return 0;
					}
				}
			}

			if(y==y2)
			{
				if(x < x2)
				{
					for(i = x + 1; i < x2; i++ )
					{
						if(brd.getInfo(i, y) !=0)
							return 0;
					}
				}

				if(x > x2)
				{
					for(i = x - 1; i > x2; i-- )
					{
						if(brd.getInfo(i, y) !=0)
							return 0;
					}
				}
			}

			if(y==y2&&x==x2)
				return 0;

			if(y!=y2&&x!=x2)
				return 0;

			if(k == 0)
			{
				return 1;
			}

			if(k < 7 && color == 2)
			{
				return 1;
			}

			if(k > 6 && color == 1)
			{
				return 1;
			}
			return 0;
		}

		public int playable(Board brd, int x2, int y2)
		{
			Point p=brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(1,x2,y2);
				else
					brd2.setSquare(7,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else 
				return 1;
		}
	}
	
	
	public class Bishop : Stone, IStone
	{
		public Bishop(int c,int i, int j)
		{
			color=c;
			x=i;
			y=j;
		}

		public int move(Board brd, int x2, int y2)
		{
			int k=brd.getInfo(x2,y2);
			int i=x2-x;
			int j=y2-y;
			if(i<0)
				i=-i;
			if(j<0)
				j=-j;
			if(i!=j)
				return 0;
			if(x2==x&&y2==y)
				return 0;
			if(x2>x&&y2>y)
			{
				for(i=x+1;i<x2;i++)
				{
					if(brd.getInfo(i, y + i - x)!=0)
						return 0;
				}
			}
			
			if(x2>x&&y2<y)
			{
				for(i=x+1;i<x2;i++)
				{
					if(brd.getInfo(i,y + x - i)!=0)
						return 0;
				}
			}

			if(x2<x&&y2>y)
			{
				for(i=x2+1;i<x;i++)
				{
					if(brd.getInfo(i, y2 + x2 - i)!=0)
						return 0;
				}
			}

			if(x2<x&&y2<y)
			{
				for(i=x2+1;i<x;i++)
				{
					if(brd.getInfo(i,y2 + i - x2)!=0)
						return 0;
				}
			}

			if(k==0)
			{
				return 1;
			}
			
			if(k<7&&color==2)
			{
				return 1;
			}

			if(k>6&&color==1)
			{
				return 1;
			}

			return 0;
		}

		public int playable(Board brd, int x2, int y2)
		{
			Point p=brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(3,x2,y2);
				else
					brd2.setSquare(9,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else 
				return 1;
		}
	}
	
	public class Queen : Stone, IStone
	{
		public Queen(int c,int i, int j)
		{
			color=c;
			x=i;
			y=j;
		}

		public int move(Board brd, int x2, int y2)
		{
			int k=brd.getInfo(x2,y2);
			int i,j;

			if(x2==x&&y2==y)
				return 0;

			if(x2==x&&y2!=y)
			{
				if(y < y2)
				{
					for(i = y + 1; i < y2; i++ )
					{
						if(brd.getInfo(x, i) !=0)
							return 0;
					}
				}

				if(y > y2)
				{
					for(i = y - 1; i > y2; i-- )
					{
						if(brd.getInfo(x, i) !=0)
							return 0;
					}
				}
			}

			else if(x2!=x&&y2==y)
			{
				if(x < x2)
				{
					for(i = x + 1; i < x2; i++ )
					{
						if(brd.getInfo(i, y) !=0)
							return 0;
					}
				}

				if(x > x2)
				{
					for(i = x - 1; i > x2; i-- )
					{
						if(brd.getInfo(i, y) !=0)
							return 0;
					}
				}
			}
			else
			{
				i=x2-x;
				j=y2-y;
			
				if(i<0)
					i=-i;
			
				if(j<0)
					j=-j;
			
				if(i!=j)
					return 0;
			
				if(x2>x&&y2>y)
				{
					for(i=x+1;i<x2;i++)
					{
						if(brd.getInfo(i, y + i - x)!=0)
							return 0;
					}
				}
			
				if(x2>x&&y2<y)
				{
					for(i=x+1;i<x2;i++)
					{
						if(brd.getInfo(i, y + x - i)!=0)
							return 0;
					}
				}

				if(x2<x&&y2>y)
				{
					for(i=x2+1;i<x;i++)
					{
						if(brd.getInfo(i, y2 + x2 - i)!=0)
							return 0;
					}
				}

				if(x2<x&&y2<y)
				{
					for(i=x2+1;i<x;i++)
					{
						if(brd.getInfo(i,y2 + i - x2)!=0)
							return 0;
					}
				}
			}
			if(k==0)	
			{
				return 1;
			}

			if(k<7&&color==2)
			{
				return 1;
			}

			if(k>6&&color==1)
			{
				return 1;
			}
			return 0;
		}

		public int playable(Board brd, int x2, int y2)
		{
			Point p=brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(4,x2,y2);
				else
					brd2.setSquare(10,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else 
				return 1;
		}
	}

	public class King : Stone, IStone
	{
		public King(int c,int i, int j)
		{
			color=c;
			x=i;
			y=j;
		}

		public int move(Board brd, int x2, int y2)
		{
			if(x2<0||y2<0)
				return 0;
			int k=brd.getInfo(x2, y2);
			int i;
			int j;
			i=x2-x;
			j=y2-y;
			if(color==1&&x==4&&y==0&&isChecked(brd)==0&&y2==y)
			{
				if(x2==x-2&&brd.getInfo(0,0)==1&&brd.getInfo(1,0)==0&&brd.getInfo(2,0)==0&&brd.getInfo(3,0)==0)
					return 2;
				if(x2==x+2&&brd.getInfo(7,0)==1&&brd.getInfo(5,0)==0&&brd.getInfo(6,0)==0)
					return 3;
			}

			else if(color==2&&x==4&&y==7&&isChecked(brd)==0&&y2==y)
			{
				if(x2==x-2&&brd.getInfo(0,7)==7&&brd.getInfo(1,7)==0&&brd.getInfo(2,7)==0&&brd.getInfo(3,7)==0)
					return 2;
				if(x2==x+2&&brd.getInfo(7,7)==7&&brd.getInfo(5,7)==0&&brd.getInfo(6,7)==0)
					return 3;
			}

			if(i<0)
				i=-i;
			
			if(j<0)
				j=-j;
			
			if(!((i==0||i==1)&&(j==0||j==1)))
				return 0;
			
			if(k==0)	
			{
				return 1;
			}

			if(k<7&&color==2)
			{
				return 1;
			}

			if(k>6&&color==1)
			{
				return 1;
			}
			return 0;
		}

		public int isChecked(Board brd2)
		{
			int c2,m;
			if(color == 1)
				c2=2;
			else 
				c2=1;
			Board b=brd2;
			int i,j;
			for(i=0;i<8;i++)
				for(j=0;j<8;j++)
					b.setSquare(brd2.getInfo(i,j),i,j);
			Pawn pawn=new Pawn(c2,0,0);
			Castle castle=new Castle(c2,0,0);
			Knight knight=new Knight(c2,0,0);
			Queen queen=new Queen(c2,0,0);
			Bishop bishop=new Bishop(c2,0,0);
			Point p=brd2.searchKing(color);
			King king=new King(c2,0,0);
			int r=p.x;
			int s=p.y;
			for(i=0;i<8;i++)
				for(j=0;j<8;j++)
				{
					m=b.getInfo(i,j);
					switch(m)
					{
						case 0:
							break;

						case 1:
							if(c2==1)
							{
								castle.x=i;
								castle.y=j;
								if(castle.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 2:
							if(c2==1)
							{
								knight.x=i;
								knight.y=j;
								if(knight.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 3:
							if(c2==1)
							{
								bishop.x=i;
								bishop.y=j;
								if(bishop.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 4:
							if(c2==1)
							{
								queen.x=i;
								queen.y=j;
								if(queen.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 5:
							if(c2==2)
							{
								king.x=i;
								king.y=j;
								if(queen.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 6:
							if(c2==1)
							{
								pawn.x=i;
								pawn.y=j;
								if(pawn.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 7:
							if(c2==2)
							{
								castle.x=i;
								castle.y=j;
								if(castle.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 8:
							if(c2==2)
							{
								knight.x=i;
								knight.y=j;
								if(knight.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 9:
							if(c2==2)
							{
								bishop.x=i;
								bishop.y=j;
								if(bishop.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 10:
							if(c2==2)
							{
								queen.x=i;
								queen.y=j;
								if(queen.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 11:
							if(c2==2)
							{
								king.x=i;
								king.y=j;
								if(king.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;

						case 12:
							if(c2==2)
							{
								pawn.x=i;
								pawn.y=j;
								if(pawn.move(b,r,s)==1)
								{
									return 1;
								}
							}
							break;
					}
				}
			return 0;
		}

		public int playable(Board brd, int x2, int y2)
		{
			if(x2<0||y2<0)
				return 0;
			Point p = brd.searchKing(color);
			King king=new King(color,0,0);
			king.x=p.x;
			king.y=p.y;
			Board brd2=new Board();
			for(int i=0;i<8;i++)
				for(int j=0;j<8;j++)
					brd2.setSquare(brd.getInfo(i,j),i,j);
			if(move(brd2,x2,y2)==1)
			{
				brd2.setSquare(0,x,y);
				if(color==1)
					brd2.setSquare(5,x2,y2);
				else
					brd2.setSquare(11,x2,y2);
			}
			else 
				return 0;
			if(king.isChecked(brd2)==1)
				return 0;
			else 
				return 1;
		}
	}
}
