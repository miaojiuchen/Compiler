#include<wordqueryobj.h>

WordQueryObj::WordQueryObj(const string &target) :m_target(target)
{
}

QueryResult WordQueryObj::eval(const TextQuerier &tq) const
{
	return tq.query(m_target);
}

string WordQueryObj::get_exp() const
{
	return m_target;
}