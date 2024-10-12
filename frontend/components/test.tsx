"use client"

import axios from "axios";
import React, { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export default function Page() {
    return (
        <div>
            <Card className="w-[350px]">
            <CardHeader>
                <CardTitle>Add Songs to Playlist</CardTitle>
                <CardDescription>Write down the playlist id and song id you want to add.</CardDescription>
            </CardHeader>
            <CardContent>
                <form>
                <div className="grid w-full items-center gap-4">
                    <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="PlaylistID">PlaylistID</Label>
                    <Input id="Pid" placeholder="Enter Playlist ID" />
                    </div>
                    <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="framework">Framework</Label>
                    <Input id="Sid" placeholder="Enter Song ID" />
                    </div>
                </div>
                </form>
            </CardContent>
            <CardFooter className="flex justify-between">
                <Button variant="outline">Cancel</Button>
                <Button>Deploy</Button>
            </CardFooter>
            </Card>
        </div>
    );
}