#pragma once

#include<m_stdafx.h>
#include<queryresult.h>

using std::map; 
using std::ifstream;

class TextQuerier
{
	friend class QueryResult;
public:
	TextQuerier(ifstream &);
	QueryResult query(const string) const;

private:
	shared_ptr<vector<string>> m_plines_vec;
	map<string, shared_ptr<set<size_t>>> m_word_map;

};