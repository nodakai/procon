#pragma once
#ifndef PYCHALC_CHALNO_INCLG
#define PYCHALC_CHALNO_INCLG

#define MAC_CONCAT(x, y) MAC_CONCAT_AUX(x, y)
#define MAC_CONCAT_AUX(x, y) x##y
#define MAC_STR(x) MAC_STR_AUX(x)
#define MAC_STR_AUX(x) #x
#define CHAL_NO_S MAC_STR(CHAL_NO)

#endif /* ifndef PYCHALC_CHALNO_INCLG */
