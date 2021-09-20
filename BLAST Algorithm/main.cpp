#include <fstream>
#include <iostream>
#include <iomanip>
#include <list>
#include <chrono>

#include "hashtable.h"
#include "vectorquery.h"

// ==================================================================
// Read

// Takes the the genome file and reads it
std::string readFile(const std::string& str){
  std::ifstream file(str);
  std::string sequences;
  std::string sequence;
  if(!file){
    std::cerr << "Unable to open: " << str << std::endl;
    exit(1);
  }
  while(file >> sequence){
    sequences += sequence;
  }
  return sequences;
}

// ==================================================================
// Main

int main() {
  std::string token, genome, timer_name, largest_timer_name;
  const std::string line(50, '-');
  const int space = 25;
  std::vector<std::pair<std::string, double> > timers;
  int table_size = 100, kmer;
  float occupancy = 0.5;
  bool construct_table = false, construct_vector = false;
  HashTable* hash_table = nullptr;
  VectorQuery* vector_query = nullptr;
  std::chrono::high_resolution_clock::time_point start, stop;

  while(std::cin >> token) {
    if(token == "genome"){
      std::string filename;
      std::cin >> filename;
      genome = readFile(filename);
    } else if(token == "table_size"){
      std::cin >> table_size;
    } else if(token == "occupancy"){
      std::cin >> occupancy;
    } else if(token == "kmer"){
      std::cin >> kmer;
    } else if(token == "start_timer"){
      std::cin >> timer_name;
      start = std::chrono::high_resolution_clock::now();
    } else if(token == "stop_timer"){
      // End the timer, and adds the time to the timers table
      stop = std::chrono::high_resolution_clock::now();
      std::chrono::duration<double> time_span = 
      std::chrono::duration_cast<std::chrono::duration<double>>(stop - start);
      std::pair<std::string, double> time;
      time.first = timer_name; 
      time.second = time_span.count();
      timers.push_back(time);
    } else if(token == "timer_table"){
      // Takes the timers and prints out the table
      for(unsigned int x = 0; x<timers.size(); x++){
        std::cout << line << std::endl;
        std::cout << "| " << timers[x].first << std::setw(space-timers[x].first.size()) << "|\t";
        std::cout << timers[x].second << " second(s)" << std::endl;
      }
      std::cout << line << std::endl;
    } else if(token == "vector_query"){
      // assumption: there should be no other command before this that initiates other commands
      if(!construct_vector){
        vector_query = new VectorQuery(genome);
        vector_query->createVector(kmer);
        construct_vector = true;
      }
      int mismatch;
      std::string query;
      std::string kmer_query;
      std::cin >> mismatch;
      std::cin >> query;
      std::cout << "Query: " << query << std::endl;
      // takes specific query from kmer and finds the query from it as the key
      kmer_query = query.substr(0, kmer);
      vector_query->findQuery(kmer_query, query, mismatch);
    } else if(token == "query"){
      // assumption: there should be no other command before this that initiates other commands
      if(!construct_table){
        hash_table = new HashTable(occupancy, table_size, genome);
        hash_table->createTable(kmer);
        construct_table = true;
      }
      int mismatch;
      std::string query;
      std::string kmer_query;
      std::cin >> mismatch;
      std::cin >> query;
      std::cout << "Query: " << query << std::endl;
      // takes specific query from kmer and finds the query from it as the key
      kmer_query = query.substr(0, kmer);
      hash_table->findQuery(kmer_query, query, mismatch);
    } else if(token == "print"){ // custom print command
      if(construct_table){
        hash_table->print();
      }
      if(construct_vector){
        vector_query->print();
      }
    } else if(token == "quit"){
      // delete the pointers associated with the constructed data structure
      if(construct_table){
        delete hash_table;
      }
      if(construct_vector){
        delete vector_query;
      }
      exit(0);
    } else {
      std::cerr << "ERROR: Unable to read token: " << token << std::endl;
    }
  }
}
