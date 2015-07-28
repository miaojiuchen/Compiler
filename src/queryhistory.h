#pragma once

#include<m_stdafx.h>
#include<queryexpression.h>

using std::vector;
using std::shared_ptr;

class QueryHistory
{
public:
	QueryHistory();

	QueryExpression &operator[](size_t index);
	size_t addRecord(QueryExpression &);

private:
	shared_ptr<vector<QueryExpression>> m_archives;

};

inline QueryHistory::QueryHistory()
	:m_archives(new vector<QueryExpression>)
{
}

