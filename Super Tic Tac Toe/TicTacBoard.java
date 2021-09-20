public class TicTacBoard{
  public String[][] board = new String[][]{{" "," "," "},{" "," "," "},{" "," "," "}};
  public String boardChar;
  public int insert(int player, int row, int col){
    if(row>3||row<1||col>3||col<1||player>2||player<1){
      insert(player,row,col);
    }
    if(board[row-1][col-1].equals(" ")){
      board[row-1][col-1] = getCharPlayer(player);
      cfw();
    }
    else{
      System.out.println("Already Occupied!");
      return player;
    }
    return 0;
  }
  
  public String getCharPlayer(int player){
    if (player == 1){
      return "X";
    }
    else return "O";
  }
  
  
  //METHOD Display board
  public void dp(){
    displayboard(this.board);
  }
  public static void displayboard(String[][] pirate){
    for(int x = 0; x<3; x++){
      System.out.print("|");
      for(int y = 0; y<3; y++){
        System.out.print(pirate[x][y]);
        System.out.print("|");
      }
      System.out.println();
      System.out.println("-------");
    }
  }
  
  //METHOD return mini board winner
  public String mbw(){
    if(boardChar.equals("X") || boardChar.equals("O")){
      return boardChar;
    }
    else{
      return null;
    }
  }
  
  
  //METHOD check for win
  public void cfw(){
    if(checkforwin(this.board)==1){
    boardChar = "X";
    }
    if(checkforwin(this.board)==2){
    boardChar = "O";
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
    //HORIZONTAL(Turns out I forgot about this the first time I made this. Took me ages to test this thing because it wouldn't read a diagonal as a victory so i assumed the rest of the code was broken. Did find some kinks but it was here all along.)
    if(board[0][0].equals(board[1][1]) && board[1][1].equals(board[2][2])){
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
    if(board[0][2].equals(board[1][1]) && board[1][1].equals(board[2][0])){
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
    return 0;
  }
//TroubleShooting
  public void setBoardChar(String a){
    boardChar = a;
  }
}