import { describe, it } from "@std/testing/bdd";
import { assertEquals, assertAlmostEquals, assertThrows } from "jsr:@std/assert";
import { Duration } from '../itsu/duration.ts';

describe('Duration', () => {
    it('creates duration from days, hours, minutes, seconds, milliseconds', () => {
        const d = new Duration(1, 2, 3, 4, 5);
        assertEquals(d.days, 1);
        assertEquals(d.hours, 2);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);
    });

    it('creates duration from milliseconds', () => {
        const d = Duration.fromMilliseconds(3723005);
        assertEquals(d.hours, 1);
        assertEquals(d.minutes, 2);
        assertEquals(d.seconds, 3);
        assertEquals(d.milliseconds, 5);
    });

    it('creates duration from minutes', () => {
        const d = Duration.fromMinutes(90);
        assertEquals(d.hours, 1);
        assertEquals(d.minutes, 30);
    });

    it('creates duration from hours', () => {
        const d = Duration.fromHours(25);
        assertEquals(d.days, 1);
        assertEquals(d.hours, 1);
    });

    it('parses duration from regex string', () => {
        let d = Duration.fromString('1d 2h 3m 4s 5ms');
        assertEquals(d.days, 1);
        assertEquals(d.hours, 2);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);

        d = Duration.fromString('1d 2h 3m');
        assertEquals(d.days, 1);
        assertEquals(d.hours, 2);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 0);
        assertEquals(d.milliseconds, 0);

        d = Duration.fromString('5h 4m');
        assertEquals(d.days, 0);
        assertEquals(d.hours, 5);
        assertEquals(d.minutes, 4);
        assertEquals(d.seconds, 0);
        assertEquals(d.milliseconds, 0);

        d = Duration.fromString('6d 7ms');
        assertEquals(d.days, 6);
        assertEquals(d.hours, 0);
        assertEquals(d.minutes, 0);
        assertEquals(d.seconds, 0);
        assertEquals(d.milliseconds, 7);
    });

    it('parses duration from delimited string with all parts', () => {
        const d = Duration.fromString('1:2:3:4:5');
        assertEquals(d.days, 1);
        assertEquals(d.hours, 2);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);
    });

    it('parses duration from delimited string with hours, minutes, seconds, milliseconds', () => {
        const d = Duration.fromString('2:3:4:5');
        assertEquals(d.days, 0);
        assertEquals(d.hours, 2);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);
    });

    it('parses duration from delimited string with minutes, seconds, milliseconds', () => {
        const d = Duration.fromString('3:4:5');
        assertEquals(d.days, 0);
        assertEquals(d.hours, 0);
        assertEquals(d.minutes, 3);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);
    });

    it('parses duration from delimited string with seconds and milliseconds', () => {
        const d = Duration.fromString('4:5');
        assertEquals(d.days, 0);
        assertEquals(d.hours, 0);
        assertEquals(d.minutes, 0);
        assertEquals(d.seconds, 4);
        assertEquals(d.milliseconds, 5);
    });

    it('parses duration from delimited string with only milliseconds', () => {
        const d = Duration.fromString('123');
        assertEquals(d.days, 0);
        assertEquals(d.hours, 0);
        assertEquals(d.minutes, 0);
        assertEquals(d.seconds, 0);
        assertEquals(d.milliseconds, 123);
    });

    it('throws error for unrecognized duration format', () => {
        assertThrows(() => Duration.fromString('not a duration'));
    });

    it('returns correct total values', () => {
        const d = new Duration(1, 2, 3, 4, 500);
        assertAlmostEquals(d.totalDays, 1 + 2 / 24 + 3 / 1440 + 4 / 86400 + 0.5 / 86400);
        assertAlmostEquals(d.totalHours, 24 + 2 + 3 / 60 + 4 / 3600 + 0.5 / 3600);
        assertAlmostEquals(d.totalMinutes, 24 * 60 + 2 * 60 + 3 + 4 / 60 + 0.5 / 60);
        assertAlmostEquals(d.totalSeconds, 24 * 3600 + 2 * 3600 + 3 * 60 + 4 + 0.5);
        assertEquals(d.totalMilliseconds, 93784500);
    });

    it('adds two durations correctly', () => {
        const d1 = new Duration(0, 1, 2, 3, 4);
        const d2 = new Duration(0, 2, 3, 4, 5);
        const sum = d1.add(d2);
        assertEquals(sum.hours, 3);
        assertEquals(sum.minutes, 5);
        assertEquals(sum.seconds, 7);
        assertEquals(sum.milliseconds, 9);
    });

    it('subtracts two durations correctly', () => {
        const d1 = new Duration(0, 3, 5, 7, 9);
        const d2 = new Duration(0, 2, 3, 4, 5);
        const diff = d1.subtract(d2);
        assertEquals(diff.hours, 1);
        assertEquals(diff.minutes, 2);
        assertEquals(diff.seconds, 3);
        assertEquals(diff.milliseconds, 4);
    });

    it('formats to string with all parts', () => {
        const d = new Duration(1, 2, 3, 4, 5);
        assertEquals(d.toString(), '1d 2h 3m 4s 5ms');
    });

    it('formats to string with only milliseconds', () => {
        const d = new Duration(0, 0, 0, 0, 123);
        assertEquals(d.toString(), '123ms');
    });

    it('formats to string with zero duration', () => {
        const d = new Duration(0, 0, 0, 0, 0);
        assertEquals(d.toString(), '0ms');
    });

    it('formats to delimited string with all parts', () => {
        const d = new Duration(1, 2, 3, 4, 5);
        assertEquals(d.toDelimitedString(), '1:2:3:4:5');
    });

    it('formats to delimited string with only milliseconds', () => {
        const d = new Duration(0, 0, 0, 0, 123);
        assertEquals(d.toDelimitedString(), '123');
    });

    it('formats to delimited string with zero duration', () => {
        const d = new Duration(0, 0, 0, 0, 0);
        assertEquals(d.toDelimitedString(), '0');
    });
});