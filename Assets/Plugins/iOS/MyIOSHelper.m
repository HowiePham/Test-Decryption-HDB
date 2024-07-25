#import "MyIOSHelper.h"

const char* _getApplicationSupportDirectory() {
    @autoreleasepool {
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSApplicationSupportDirectory, NSUserDomainMask, YES);
        NSString *applicationSupportDirectory = [paths objectAtIndex:0];
        return [applicationSupportDirectory UTF8String];
    }
}