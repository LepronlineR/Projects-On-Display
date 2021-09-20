#include "vectorquery.h"

#include <iostream>

// Constructor
// =======================================================

VectorQuery::VectorQuery(const std::string& g){
    genome = g;
    vector = vector_type();
}

// Public methods
// =======================================================

/* Create table by incrementing starting at the k_mer size, then looping
   through the next genome, a key will be inserted. When the vector is full
   it will resize by itself
*/
void VectorQuery::createVector(int k_mer){
    unsigned int start = 0;
    while(start != genome.size()-k_mer+1){
        std::string key = genome.substr(start, k_mer);
        insert(key, start, vector);
        start++;
    }
}

/* Iterates through the entire table and prints it out
   NOTE: This function is used for testing

   One index may be...
   (Genome) 
   ===============================
   (positions)
   --------------------------------------
   TCTCATCAGC
   ===============================
   23, 210, 5003,
*/
void VectorQuery::print(){
    for(vector_type::iterator vItr = vector.begin();
    vItr != vector.end(); vItr++){
        std::cout << vItr->first << std::endl;
        std::cout << "==================================" << std::endl;
        std::list<unsigned int> re = vItr->second;
        for(std::list<unsigned int>::iterator aItr = re.begin();
        aItr != re.end(); aItr++){
            std::cout << *aItr << ", ";
        }
        std::cout << std::endl << std::endl;
    }
    std::cout << std::endl;
}

/* The query looks for the indexes of that possible query given the kmers, through the entire vector. 
   Then it will iterate the possible indexes with a given start in the genome,
   a mismatch is compared, and then counted, if that mismatch is less than the amount
   of mismatch the user inputted, then it will print it out. Otherwise, it will be ignored.
   If there are no queries avalible, then it will have "No Match"
*/
void VectorQuery::findQuery(const std::string& kmer_query, const std::string& query, int mismatch){
    int count = 0;
    for(unsigned int x = 0; x<vector.size(); x++){
        if(kmer_query == vector[x].first){
            std::list<unsigned int> genomeLocation = vector[x].second;
            for(std::list<unsigned int>::iterator gItr = genomeLocation.begin();
            gItr != genomeLocation.end(); gItr++){
                std::string expectedQuery = genome.substr(*gItr, query.size());
                int mismatched = compareMismatches(query, expectedQuery, mismatch);
                if(mismatched <= mismatch){
                    count++;
                    std::cout << *gItr << ' ' << mismatched << ' ' << expectedQuery << std::endl;
                }
            }
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
int VectorQuery::compareMismatches(const std::string& str, const std::string& compared, int mismatch){
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

// Loops through the vector to find similar keys, if there is one, then it will 
// push back the index value in that key, if not then it will create a new
// pair of that key and the index value associated with the key
void VectorQuery::insert(const std::string& key, unsigned int index, vector_type& v){
    for(unsigned int x = 0; x<v.size(); x++){
        if(v[x].first == key){ // key exists
            v[x].second.push_back(index);
            return;
        }
    }
    // the key does not exist, and a new pair will be created
    std::list<unsigned int> temp;
    temp.push_back(index);
    std::pair<std::string, std::list<unsigned int> > pairing;
    pairing.first = key;
    pairing.second = temp;
    v.push_back(pairing);
}