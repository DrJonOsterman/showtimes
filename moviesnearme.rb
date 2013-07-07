require 'open-uri'
require 'nokogiri'

#doc = Nokogiri::HTML(open("http://www.google.com/movies?near=11368'"))
doc = Nokogiri::HTML(open("movDump.htm"))
#puts doc.css("#movie_results .theater .showtimes .show_left .movie .name")

doc.css("#movie_results .movie_results .theater").each do |theater|

	puts "------Theater------"
	puts theater.css(".desc .name a").text()

	theater.css(".showtimes .show_left .movie").each do |leftMov|
		puts leftMov.css(".name").text()

		leftMov.css(".times span").each do |showTime|
			puts "time should be here" # puts showTime.css("a").text()
		end

	end

	theater.css(".showtimes .show_right .movie").each do |rightMov|
		puts rightMov.css(".name").text()

		rightMov.css(".times span").each do |showTime|
			puts "time should be here" # puts showTime.css("a").text()
		end

	end
	puts "-------------------"
end