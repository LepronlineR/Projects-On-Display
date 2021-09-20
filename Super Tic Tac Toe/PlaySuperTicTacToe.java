import java.util.Scanner;
public class PlaySuperTicTacToe{
  public static Scanner sc = new Scanner(System.in);
  public static boolean initializeAI = false;
  public static TicTacBoard[][] stboard = new TicTacBoard[][]{{new TicTacBoard(),new TicTacBoard(),new TicTacBoard()},{new TicTacBoard(),new TicTacBoard(),new TicTacBoard()},{new TicTacBoard(),new TicTacBoard(),new TicTacBoard()}};
  public static String[][] valueboard = new String[][]{{" "," "," "},{" "," "," "},{" "," "," "}};
  public static int lowestVal = 100;
  public static int savedVal = 0;
  public static void updateVB(){
    for(int x = 0; x<3; x++){

      for(int y = 0; y<3; y++){

        if(stboard[x][y].boardChar=="X"||stboard[x][y].boardChar=="O"){

            valueboard[x][y] = stboard[x][y].mbw();
        }
      }
    }
  }
  public static int mrowinplay = 1;
  public static int mcolinplay = 1;
  //PlayerOne
  public static void playerOne(int k, int j){
    System.out.println("P L A Y E R O N E");
    System.out.println("Megaboard:");
    dboc(valueboard);
    System.out.println("Miniboard:");
    dboc(stboard[mrowinplay-1][mcolinplay-1].board);
    System.out.println("Choose MiniBoard Row(1-3)");
    int p = sc.nextInt();
    System.out.println("Choose MiniBoard Column(1-3)");
    int o = sc.nextInt();
    //Check for validity
    if(k>3|| k<1|| j>3|| j<1|| p>3|| p<1|| o>3|| o<1){
      System.out.println("Invalid Entries");
      playerOne(k, j);
    }
    if(stboard[k-1][j-1].insert(1, p, o)==1){
      playerOne(k, j);
    }
    
    stboard[k-1][j-1].cfw();
    updateVB();
    cfmw();
    if(valueboard[k-1][j-1]=="X"|| valueboard[k-1][j-1]=="O"){
      play();
    }
    
  }
  //PlayerTwo
   public static void playerTwo(int k, int j){
    System.out.println("P L A Y E R T W O");
    System.out.println("Megaboard:");
    dboc(valueboard);
    System.out.println("Miniboard:");
    dboc(stboard[mrowinplay-1][mcolinplay-1].board);
    System.out.println("Choose MiniBoard Row(1-3)");
    int p = sc.nextInt();
    System.out.println("Choose MiniBoard Column(1-3)");
    int o = sc.nextInt();
    
    //Check for validity
    if(k>3|| k<1|| j>3|| j<1|| p>3|| p<1|| o>3|| o<1){
      System.out.println("Invalid Entries");
      playerTwo(k, j);
    }
    
    if(stboard[k-1][j-1].insert(2, p, o)==2){
      playerTwo(k, j);
    }
    stboard[k-1][j-1].cfw();
    updateVB();
    cfmw();
  }
   //fake player two
   public static void playerTwo(int k, int j, int p, int o){
    System.out.println("P L A Y E R T W O");
    System.out.println("Megaboard:");
    dboc(valueboard);
    System.out.println("Miniboard:");
    dboc(stboard[mrowinplay-1][mcolinplay-1].board);
    if(stboard[k-1][j-1].insert(2, p+1, o+1)==2){
      playerTwo(k, j, p, o);
    }
    stboard[k-1][j-1].cfw();
    updateVB();
    cfmw();
  }
   
   
//METHOD (PRIMARY) Play
   public static void play(){
    chooseMBoard();
    while(1<3){
      if(initializeAI==true){
        playerOne(mrowinplay,mcolinplay);
        skynet(stboard[mrowinplay-1][mcolinplay-1].board);
      } else {
        playerOne(mrowinplay,mcolinplay);
        playerTwo(mrowinplay,mcolinplay);
      }
    }
   }
   
   public static void chooseMBoard(){
     System.out.println("MEGABOARD:");
     dboc(valueboard);
     System.out.println("Choose Megaboard Row");
     mrowinplay = sc.nextInt();
     System.out.println("Choose Megaboard Column");
     mcolinplay = sc.nextInt();
     if(valueboard[mrowinplay-1][mcolinplay-1] =="X" ||valueboard[mrowinplay-1][mcolinplay-1] =="O" ){
       System.out.println("Already Played on this Board");
       chooseMBoard();
     }
   }
   
  
   
//DisplayBoardofChoice
   public static void dboc(String[][] pirate){
    for(int x = 0; x<3; x++){
      for(int y = 0; y<3; y++){
        System.out.print(pirate[x][y]);
        if(x+y<4){
          System.out.print("|");
        }
        if(x==2 && y==2){
          System.out.print("|");
        }
      }
      System.out.println();
      if(x<2){
        System.out.println("-------");
      }
    }
  }
   
  //METHOD Check for MEGAWIN
  public static void cfmw(){
    if(checkforwin(valueboard)==1){
    winner(1);
    }
    if(checkforwin(valueboard)==2){
    winner(2);
    }
  }
  public static int checkforwin(String[][] board){
    //hori
    if(board[0][0].equals(board[0][1]) && board[0][1].equals(board[0][2])){
      if(board[0][0].equals("X")){
        return 1;
      }
      else if(board[0][0].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
      
    }
    if(board[1][0].equals(board[1][1]) && board[1][1].equals(board[1][2])){
      if(board[1][0].equals("X")){
        return 1;
      }
      else if(board[1][0].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
    }
    if(board[2][0].equals(board[2][1]) && board[2][1].equals(board[2][2])){
      if(board[2][0].equals("X")){
        return 1;
      }
      else if(board[2][0].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
    }
    //vert
    if(board[0][0].equals(board[1][0]) && board[1][0].equals(board[2][0])){
      if(board[0][0].equals("X")){
        return 1;
      }
      else if(board[0][0].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
    }
    if(board[0][1].equals(board[1][1]) && board[1][1].equals(board[2][1])){
      if(board[0][1].equals("X")){
        return 1;
      }
      else if(board[0][1].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
    }
    if(board[0][2].equals(board[1][2]) && board[1][2].equals(board[2][2])){
      if(board[0][2].equals("X")){
        return 1;
      }
      else if(board[0][2].equals("O")){
        return 2;
      }
      else{
        return 0;
      }
    }
    return 0;
  }
  //Winner!
  public static void winner(int x){
    if(x == 1){
      System.out.println("Player 1 Wins!");
    }
    if(x == 2){
      System.out.println("Player 2 Wins!");
    }
  }
  
  //AI implementation
  
  public static int point(String[][] board){
    //Check Win Diagonal
    if(board[0][0] == board[1][1] && board[1][1] == board[2][2]){
      if(board[0][0].equals("X"))
        return +10;
      else if(board[0][0].equals("O"))
        return -10;
    }
    if(board[0][2] == board[1][1] && board[1][1] == board[2][0]){
      if(board[0][2].equals("X"))
        return +10;
      else if(board[0][2].equals("O"))
        return -10;
    }
    for(int x=0; x<3; x++){
      //Check Win Row
      if(board[x][0] == board[x][1] && board[x][1] == board[x][2]){
        if(board[x][0].equals("X"))
          return +10;
        else if(board[x][0].equals("O"))
          return -10;
      }
    }
    for(int x=0; x<3; x++){
      //Check Win Column
      if(board[0][x] == board[1][x] && board[1][x] == board[2][x]){
        if(board[0][x].equals("X"))
          return +10;
        else if(board[0][x].equals("O"))
          return -10;
      }
    }
    return 0;
  }
  
  public static boolean remainingMoves(String[][] board){
    for(int x=0; x<3; x++){
      for(int y=0; y<3; y++){
        if(board[x][y].equals(" "))
          return true;
      }
    }
    return false;
  }
  
  public static int minmax(String[][] board, int value, boolean maxing){
    int numPoint = point(board);
    if(numPoint==10){
      savedVal = value;
      return 10;
    }
    if(numPoint==-10){
      savedVal = value;
      return -10;
    }
    if(remainingMoves(board)==false)
      return 0;
    
    if(maxing){
      int save = -100;
      for(int x=0; x<3;x++){
        for(int y=0; y<3; y++){
          if(board[x][y].equals(" ")){
            board[x][y] = "X";
            save = Math.max(save, minmax(board,value+1,!maxing));
            board[x][y] = " ";
          }
        }
      }
      return save;
    } else {
      int save = 100;
      for(int x=0; x<3;x++){
        for(int y=0; y<3; y++){
          if(board[x][y].equals(" ")){
            board[x][y] = "O";
            save = Math.min(save, minmax(board,value+1,!maxing));
            board[x][y] = " ";
          }
        }
      }
      return save;
    }                    
  }
  public static void skynet(String[][] board){
    int numba = -100;
    int bestRow = -1;
    int bestCol = -1;
    for(int x=0; x<3; x++){
      for(int y=0; y<3; y++){
        if(board[x][y].equals(" ")){
          board[x][y] = "X";
          int move = minmax(board, 0, false);
          board[x][y] = " ";
          if(move>numba && lowestVal>savedVal){
            bestRow = x;
            bestCol = y;
            numba = move;
          }
          lowestVal = savedVal;
        }
      }
    }
    playerTwo(mrowinplay,mcolinplay,bestRow,bestCol);
  }
  
  public static void main(String[] args){
    System.out.println("Type 1 to play with two players, Type 2 to play with AI");
    int playerChoose = sc.nextInt();
    if(playerChoose == 1){
      initializeAI = false;
      play();
    }else if(playerChoose == 2){
      initializeAI = true;
      play();
    }else
      System.out.println("Entered wrong, restart console");
  }
}