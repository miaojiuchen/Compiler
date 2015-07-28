#include<queryhistory.h>

size_t QueryHistory::addRecord(QueryExpression &qe)
{
	m_archives->push_back(qe);
	return m_archives->size() - 1;
}

QueryExpression &QueryHistory::operator[](size_t index)
{
	return (*m_archives)[index];
}