#include<binaryqueryobj.h>

string BinaryQueryObj::get_exp() const
{
	return "(" + m_lexp.get_exp() + " " + exp_symbol + " " + m_rexp.get_exp() + ")";
}