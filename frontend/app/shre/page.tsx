"use client"

import axios from "axios";
import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useToast } from "@/hooks/use-toast";
import DLayout from "@/components/DLayout";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
  } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
  
interface PlayListSong {
    Playlist: {
        Name: string;
    };
    Song: {
        Id: number;
        Title: string;
        Artist: string;
        Album: string;
        Lyrics: string;
    };
}

export default function Test() {
    const { register ,watch} = useForm();
    const { register: button1 , handleSubmit: subBtn1 } = useForm();
    const { register: button2 , handleSubmit: subBtn2 } = useForm();
    const { toast } = useToast();

    const [PlayListSongData, setPlayListSongData] = useState<PlayListSong | PlayListSong[]>([]);
    var toBeSearched = watch("Name");

    useEffect(() => {
        (async () => {
            try {
                if (!toBeSearched) {
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/getall`);
                    setPlayListSongData(PResponse.data);
                } else {
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/show/${toBeSearched}`);
                    setPlayListSongData(RResponse.data);
                }
            } catch (error) {
                console.error("Error is :: ", error);
                setPlayListSongData([]);
            }
        })();
    }, [toBeSearched]);

    const submitbybtn1 = async (data: any) => {
        try {
            if (!data.shuffle) {
                toast({
                    title: "Field is empty",
                });
                return;
            } else {
                const SResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/${data.shuffle}/shuffle`);
                setPlayListSongData(SResponse.data);
            }
        } catch (error) {
            console.error("Error is :: ", error);
            setPlayListSongData([]);
        }
    };

    const submitbybtn2 = async (data: any) => {
        try {
            if (!data.plname || !data.songid || !data.repeat) {
                toast({
                    title: "Field is empty",
                });
                return;
            }

            try{
                let response;

                if(data.repeat === "song") {
                    response = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/${data.plname}/next/${data.songid}?repeatSong=${true}`);
                } else if (data.repeat === "list") {
                    response = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/${data.plname}/next/${data.songid}?repeatPlaylist=${true}`);
                }
    
                setPlayListSongData([response?.data]);
            }catch(error: any){
                if (error.response && error.response.status === 500) {
                    toast({
                      variant: "destructive",
                      title: "Song is out of Playlist!",
                    });
                  } else {
                    toast({
                      variant: "destructive",
                      title: "An error occurred!",
                      description: "Please try again later.",
                    });
                }
            }
        } catch (error:any) {
            toast({
                variant: "destructive",
                title: "An error occurred!",
                description: "Please try again later.",
              });
            setPlayListSongData([]);
        }
    };

    return (
        <DLayout>
            <div className="max-w-1xl mx-auto bg-white rounded-lg shadow-lg p-6">

                <div className="flex w-full space-x-4">
                    {/* First Search Box with Button */}
                    <div className="flex flex-1">
                        <input
                            type="text"
                            placeholder="Playlist Name"
                            {...button1('shuffle')}
                            className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-l-md shadow-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                        />
                        <Button
                            className="rounded-r-md text-white bg-indigo-600 hover:bg-indigo-700 px-6 py-2 ml-2"
                            onClick={subBtn1(submitbybtn1)}
                        >
                            Shuffle
                        </Button>
                    </div>

                    <div className="border-2 border-indigo-600 h-auto mx-2"></div>

                    {/* Second Search Box with Button */}
                    <div className="flex flex-1">
                        <input
                            type="text"
                            placeholder="P.list Name"
                            {...button2('plname')}
                            className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-l-md shadow-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                        />
                        <input
                            type="text"
                            placeholder="Song ID"
                            {...button2('songid')}
                            className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-l-md shadow-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                        />
                        <select
                            {...button2("repeat")}
                            className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        >
                            <option value="">Select</option>
                            <option value="song">Repeat Song</option>
                            <option value="list">Repeat List</option>
                        </select>
                        <Button
                            className="rounded-r-md text-white bg-indigo-600 hover:bg-indigo-700 px-6 py-2 ml-2"
                            onClick={subBtn2(submitbybtn2)}
                        >
                            Repeat
                        </Button>
                    </div>
                </div>

                <br/>
                <input
                    type="text"
                    placeholder="Search for Playlist by Name"
                    {...register('Name')}
                    className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md shadow-lg ring-2 ring-indigo-500 focus:ring-4 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                />
                <br/>

            {/* Viewing the Data */}
            <Table>
                <TableCaption>A list of Song from all Playlist.</TableCaption>
                <TableHeader>
                    <TableRow>
                        <TableHead className="w-[100px]">Playlist Name</TableHead>
                        <TableHead>Song ID</TableHead>
                        <TableHead>Song Title</TableHead>
                        <TableHead>Song Artist</TableHead>
                        <TableHead>Song Album</TableHead>
                        <TableHead className="text-right">Song Lyrics</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {Array.isArray(PlayListSongData) ? (
                        PlayListSongData.map((data, key) => (
                            <TableRow key={key}>
                                <TableCell className="font-medium">{data.Playlist.Name}</TableCell>
                                <TableCell>{data.Song.Id}</TableCell>
                                <TableCell>{data.Song.Title}</TableCell>
                                <TableCell>{data.Song.Artist}</TableCell>
                                <TableCell>{data.Song.Album}</TableCell>
                                <TableCell className="text-right">{data.Song.Lyrics}</TableCell>
                            </TableRow>
                        ))
                    ) : (
                        <TableRow>
                            <TableCell className="font-medium">{PlayListSongData.Playlist.Name}</TableCell>
                            <TableCell>{PlayListSongData.Song.Id}</TableCell>
                            <TableCell>{PlayListSongData.Song.Title}</TableCell>
                            <TableCell>{PlayListSongData.Song.Artist}</TableCell>
                            <TableCell>{PlayListSongData.Song.Album}</TableCell>
                            <TableCell className="text-right">{PlayListSongData.Song.Lyrics}</TableCell>
                        </TableRow>
                    )}
                </TableBody>
            </Table>

            {/* End of Viewing Data */}

            </div>
        </DLayout>
    );
}