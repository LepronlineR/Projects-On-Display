#include "hashtable.h"

#include <iostream>

// Constructor
// =======================================================

HashTable::HashTable(float o, int ts, const std::string& g){
    genome = g;
    occupancy = o;
    occupied = 0;
    table = table_type(ts);
}

// Hash Function
// =======================================================

/* 
DJB Hash Function

An algorithm produced by Professor Daniel J. Bernstein 
and shown first to the world on the usenet newsgroup comp.lang.c. 
It is one of the most efficient hash functions ever published.

Source: https://www.partow.net/programming/hashfunctions/#top
*/
unsigned int HashTable::hashFunction(const std::string& str){
    unsigned int hash = 5381;
    unsigned int x = 0;
    for(x = 0; x < str.size(); x++){
        hash = ((hash << 5) + hash) + (str[x]);
    }
    return hash;
}

// Public methods
// =======================================================

/* Create table by incrementing starting at the k_mer size, then looping
   through the next genome, a key will be inserted. Before inserting,
   the method resizes
   When to resize:
        When the current occupancy is lower than the amount of expected occupancy, then it will be resized
*/
void HashTable::createTable(int k_mer){
    unsigned int start = 0;
    while(start != genome.size()-k_mer+1){
        std::string key = genome.substr(start, k_mer);
        unsigned int index = hashFunction(key) % table.size();
        if(calculateOccupancy() > occupancy){
            resize();
        }
        insert(key, start, index, table);
        start++;
    }
}

/* Iterates through the entire table and prints it out, empty spaces are ignored
   NOTE: This function is used for testing

   One index may be...
   (Genome) (Hash Number)
   ===============================
   (positions)
   --------------------------------------
   TCTCATCAGC 1317275702
   ===============================
   23, 210, 5003,
*/
void HashTable::print(){
    for(table_type::iterator tItr = table.begin();
    tItr != table.end(); tItr++){
        if(hashFunction(tItr->first) != 5381){ // not empty
            std::cout << tItr->first << ' ' << hashFunction(tItr->first) << std::endl;
            std::cout << "==================================" << std::endl;
            std::list<unsigned int> re = tItr->second;
            for(std::list<unsigned int>::iterator aItr = re.begin();
            aItr != re.end(); aItr++){
                std::cout << *aItr << ", ";
            }
            std::cout << std::endl << std::endl;
        }
    }
    std::cout << std::endl;
}

/* The query looks for the indexes of that possible query given the kmers.
   Then it will iterate the possible indexes with a given start in the genome,
   a mismatch is compared, and then counted, if that mismatch is less than the amount
   of mismatch the user inputted, then it will print it out. Otherwise, it will be ignored.
   If there are no queries avalible, then it will have "No Match"
*/
void HashTable::findQuery(const std::string& kmer_query, const std::string& query, int mismatch){
    bool obtained; // helper to find out if the genome is obtained
    int count = 0;
    std::list<unsigned int> genomeLocation = getIndexes(kmer_query, obtained);
    if(!obtained){
        std::cout << "No Match" << std::endl;
        return;
    }
    for(std::list<unsigned int>::iterator gItr = genomeLocation.begin();
    gItr != genomeLocation.end(); gItr++){
        // takes the index of the genome and finds any mismatches
        std::string expectedQuery = genome.substr(*gItr, query.size());
        int mismatched = compareMismatches(query, expectedQuery, mismatch);
        if(mismatched <= mismatch){
            count++;
            std::cout << *gItr << ' ' << mismatched << ' ' << expectedQuery << std::endl;
        }
    }
    if(count == 0){
        std::cout << "No Match" << std::endl;
    }
}

// Private helper methods
// =======================================================

// Get the string, and the string being compared. Then from the amount of mismatches,
// it will loop through the characters to determine if 
int HashTable::compareMismatches(const std::string& str, const std::string& compared, int mismatch){
    int count = 0;
    for(unsigned int x = 0; x<str.size(); x++){
        if(str[x] != compared[x]){
            count++;
            if(count > mismatch){
                return count;
            }
        }
    }
    return count;
}

// get index from hash function, if the index is not exactly the same, then
// start to iterate through the table
std::list<unsigned int> HashTable::getIndexes(const std::string& str, bool& obtained){
    unsigned int index = hashFunction(str) % table.size();
    unsigned int stopper = index; // in the case where the genome string does not exist, stop the loop
    while(true){
        if(table[index].first == str){
            obtained = true;
            return table[index].second;
        }
        if(index == table.size()-1){
            // maybe index is all the way back... restart from index 0
            index = 0;
        }
        // due to linear probing, the genome can be in the next indexes
        index++;
        if(index == stopper){ // went through the entire table... did not obtain the genome
            obtained = false;
            return table[index].second; 
        }
    }
}

// The table is resized by creating a new table with a new table size,
// then every element is transfered over based on the new hashtable index
void HashTable::resize(){
    unsigned int new_table_size = 2 * table.size();
    table_type n_table = table_type(new_table_size);
    for(table_type::iterator tItr = table.begin(); tItr != table.end(); tItr++){
        if(!tItr->first.empty()){ // faster since it skips empty keys
            unsigned int index = hashFunction(tItr->first) % n_table.size();
            while(!n_table[index].first.empty()){ // finds next free space, same as insert
                index++;
                if(index == n_table.size()){
                    // index is full at the end... restart from index 0
                    index = 0;
                }
            }
            n_table[index] = *tItr;
        }
    }
    table = n_table;
}

// Inserting through the hashtable and then incrementing the occupancy.
// A loop is applied through the table as it inserts and checks for possible collisions.
void HashTable::insert(const std::string& key, unsigned int index, unsigned int pos, table_type& tb){
    while(true){
        if(tb[pos].second.empty()){ // takes the index of the hash table and inserts index of the genome
            tb[pos].first = key;
            increaseOccupancy();
            tb[pos].second.push_back(index);
            return;
        } else if(table[pos].first == key){ // similar key, so push back index of genome
            tb[pos].second.push_back(index);
            return;
        }
        if(pos == tb.size()-1){
            // index is full at the end... restart from index 0
            pos = 0;
        }
        pos++;
    }
}