/* tslint:disable */
/* eslint-disable */
/**
 * JobCounselor API
 * Minimal API surface for the JobCounselor backend
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface Language
 */
export interface Language {
    /**
     * 
     * @type {string}
     * @memberof Language
     */
    name?: string;
    /**
     * 
     * @type {string}
     * @memberof Language
     */
    proficiency?: string;
}

/**
 * Check if a given object implements the Language interface.
 */
export function instanceOfLanguage(value: object): value is Language {
    return true;
}

export function LanguageFromJSON(json: any): Language {
    return LanguageFromJSONTyped(json, false);
}

export function LanguageFromJSONTyped(json: any, ignoreDiscriminator: boolean): Language {
    if (json == null) {
        return json;
    }
    return {
        
        'name': json['name'] == null ? undefined : json['name'],
        'proficiency': json['proficiency'] == null ? undefined : json['proficiency'],
    };
}

export function LanguageToJSON(json: any): Language {
    return LanguageToJSONTyped(json, false);
}

export function LanguageToJSONTyped(value?: Language | null, ignoreDiscriminator: boolean = false): any {
    if (value == null) {
        return value;
    }

    return {
        
        'name': value['name'],
        'proficiency': value['proficiency'],
    };
}

